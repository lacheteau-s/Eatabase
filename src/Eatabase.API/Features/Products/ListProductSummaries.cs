using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Eatabase.API.Features.Products;

internal static class ListProductSummaries
{
	public static void Register(IEndpointRouteBuilder router)
	{
		router.MapGet("/products", Endpoint);
	}

	private static Ok<List<ProductSummary>> Endpoint(
		[FromServices] ListProductSummariesRequestHandler handler
	)
	{
		var summaries = handler.Handle();

		return TypedResults.Ok(summaries);
	}
}

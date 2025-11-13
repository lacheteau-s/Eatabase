using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Eatabase.API.Features.Products;

internal static class ListProductSummaries
{
	public static void Register(IEndpointRouteBuilder router)
	{
		router.MapGet("/products", Endpoint);
	}

	private static async Task<Ok<List<ProductSummary>>> Endpoint(
		[FromServices] ListProductSummariesRequestHandler handler,
		CancellationToken ct
	)
	{
		var summaries = await handler.HandleAsync(ct);

		return TypedResults.Ok(summaries);
	}
}

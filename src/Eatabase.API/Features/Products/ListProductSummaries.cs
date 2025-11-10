using Microsoft.AspNetCore.Http.HttpResults;

namespace Eatabase.API.Features.Products;

internal static class ListProductSummaries
{
	public static void Register(IEndpointRouteBuilder router)
	{
		router.MapGet("/products", Endpoint);
	}

	private static Ok Endpoint()
	{
		return TypedResults.Ok();
	}
}

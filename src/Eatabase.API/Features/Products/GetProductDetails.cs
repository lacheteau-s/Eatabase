using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Eatabase.API.Features.Products;

internal static class GetProductDetails
{
	public static void Register(IEndpointRouteBuilder router)
	{
		router.MapGet("/products/{id:guid}", Endpoint);
	}

	private static async Task<Results<NotFound, Ok<ProductDetails>>> Endpoint(
		[FromServices] GetProductDetailsRequestHandler handler,
		[FromRoute] Guid id,
		CancellationToken ct
	)
	{
		var result = await handler.HandleAsync(id, ct);

		return result.IsFailure
			? TypedResults.NotFound()
			: TypedResults.Ok(result.Value);
	}
}

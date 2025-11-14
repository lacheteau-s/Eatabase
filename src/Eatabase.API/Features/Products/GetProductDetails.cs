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
		var product = await handler.HandleAsync(id, ct);

		if (product is null)
			return TypedResults.NotFound();

		return TypedResults.Ok(product);
	}
}

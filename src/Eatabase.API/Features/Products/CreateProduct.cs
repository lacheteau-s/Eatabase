using Microsoft.AspNetCore.Http.HttpResults;

namespace Eatabase.API.Features.Products;

internal static class CreateProduct
{
	public static void Register(IEndpointRouteBuilder router)
	{
		router.MapPost("/products", Endpoint);
	}

	private static Ok Endpoint(CreateProductRequest request)
	{
		return TypedResults.Ok();
	}
}

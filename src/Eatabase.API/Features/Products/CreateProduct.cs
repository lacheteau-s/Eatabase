using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Eatabase.API.Features.Products;

internal static class CreateProduct
{
	private const string _route = "/products";

	public static void Register(IEndpointRouteBuilder router)
	{
		router.MapPost(_route, Endpoint);
	}

	private static async Task<Created<Guid>> Endpoint(
		CancellationToken ct,
		[FromServices] CreateProductRequestHandler handler,
		[FromBody] CreateProductRequest request
	)
	{
		var id = await handler.HandleAsync(request, ct);

		return TypedResults.Created($"{_route}/{id}", id);
	}
}

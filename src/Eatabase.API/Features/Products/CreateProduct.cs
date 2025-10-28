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

	private static Created<Guid> Endpoint(
		[FromServices] CreateProductRequestHandler handler,
		[FromBody] CreateProductRequest request)
	{
		var id = handler.Handle(request);

		return TypedResults.Created($"{_route}/{id}", id);
	}
}

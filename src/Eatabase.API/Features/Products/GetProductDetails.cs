using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Eatabase.API.Features.Products;

internal static class GetProductDetails
{
	public static void Register(IEndpointRouteBuilder router)
	{
		router.MapGet("/products/{id:guid}", Endpoint);
	}

	private static Ok Endpoint([FromRoute] Guid id)
	{
		return TypedResults.Ok();
	}
}

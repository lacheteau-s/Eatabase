using FluentValidation;
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

	private static async Task<Results<ValidationProblem, Conflict, Created<Guid>>> Endpoint(
		[FromServices] IValidator<CreateProductRequest> validator,
		[FromServices] CreateProductRequestHandler handler,
		[FromBody] CreateProductRequest request,
		CancellationToken ct
	)
	{
		var validationResult = validator.Validate(request);

		if (!validationResult.IsValid)
			return TypedResults.ValidationProblem(validationResult.ToDictionary());

		var result = await handler.HandleAsync(request, ct);

		return result.IsFailure
			? TypedResults.Conflict()
			: TypedResults.Created($"{_route}/{result.Value}", result.Value);
	}
}

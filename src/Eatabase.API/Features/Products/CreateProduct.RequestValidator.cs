using System.Linq.Expressions;
using System.Numerics;
using FluentValidation;

namespace Eatabase.API.Features.Products;

internal sealed class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
	private static class ValidationConstants
	{
		public const int BrandMaxLength = 50;
		public const int NameMaxLength = 100;
		public const int ServingSizeMaxLength = 25;
		public static readonly Bounds<int> Calories = new(0, 1000);
		public static readonly Bounds<decimal> Macro = new(0m, 100m);
	}

	private record Bounds<T>(T Min, T Max) where T : INumber<T>;

	public CreateProductRequestValidator()
	{
		ValidateString(p => p.Brand, ValidationConstants.BrandMaxLength);
		ValidateString(p => p.Name, ValidationConstants.NameMaxLength);
		ValidateString(p => p.ServingSize, ValidationConstants.ServingSizeMaxLength);
		ValidateString(p => p.ServingSizeMetric, ValidationConstants.ServingSizeMaxLength);

		ValidateMacro(p => p.Calories, ValidationConstants.Calories);
		ValidateMacro(p => p.TotalFat, ValidationConstants.Macro);
		ValidateMacro(p => p.SaturatedFat, ValidationConstants.Macro);
		ValidateMacro(p => p.TransFat, ValidationConstants.Macro);
		ValidateMacro(p => p.TotalCarbs, ValidationConstants.Macro);
		ValidateMacro(p => p.Sugars, ValidationConstants.Macro);
		ValidateMacro(p => p.Fiber, ValidationConstants.Macro);
		ValidateMacro(p => p.Protein, ValidationConstants.Macro);
	}
	
	private IRuleBuilderOptions<CreateProductRequest, string> ValidateString(
		Expression<Func<CreateProductRequest, string>> selector, int maxLength
	) =>
		RuleFor(selector)
			.NotEmpty()
			.MaximumLength(maxLength);

	private IRuleBuilderOptions<CreateProductRequest, T?> ValidateMacro<T>(
		Expression<Func<CreateProductRequest, T?>> selector,
		Bounds<T> bounds
	) where T : struct, INumber<T> =>
		RuleFor(selector)
			.GreaterThanOrEqualTo(bounds.Min)
			.LessThan(bounds.Max);
}

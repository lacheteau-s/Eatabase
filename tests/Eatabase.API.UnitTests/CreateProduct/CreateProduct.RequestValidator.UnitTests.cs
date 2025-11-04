using Eatabase.API.Features.Products;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace Eatabase.API.UnitTests.CreateProduct;

using TestData = CreateProductRequestValidatorTestData;

public sealed class CreateProductRequestValidatorUnitTests
{
	private readonly CreateProductRequestValidator _validator = new();

	[Theory]
	[MemberData(nameof(TestData.ValidRequests), MemberType = typeof(TestData))]
	internal void Validate_With_ValidRequest_Succeeds(CreateProductRequest request)
	{
		// Act
		var result = _validator.TestValidate(request);

		// Assert
		result.ShouldNotHaveAnyValidationErrors();
	}

	[Theory]
	[MemberData(nameof(TestData.StringFieldsNullOrEmpty), MemberType = typeof(TestData))]
	internal void Validate_With_StringFieldsNullOrEmpty_Fails(CreateProductRequest request)
	{
		// Act
		var result = _validator.TestValidate(request);

		// Assert
		result.Errors.Should().HaveCount(4);

		result.Errors.Select(x => x.PropertyName).Should().BeEquivalentTo([
			nameof(CreateProductRequest.Brand),
			nameof(CreateProductRequest.Name),
			nameof(CreateProductRequest.ServingSize),
			nameof(CreateProductRequest.ServingSizeMetric)
		]);

		result.Errors.Should().AllSatisfy(x =>
			x.ErrorMessage.Should().Match("'*' must not be empty.")
		);
	}

	[Fact]
	internal void Validate_With_StringPropertiesTooLong_Fails()
	{
		// Act
		var result = _validator.TestValidate(TestData.StringFieldsTooLong);

		// Assert
		result.Errors.Should().HaveCount(4);

		result.Errors.Select(x => x.PropertyName).Should().BeEquivalentTo([
			nameof(CreateProductRequest.Brand),
			nameof(CreateProductRequest.Name),
			nameof(CreateProductRequest.ServingSize),
			nameof(CreateProductRequest.ServingSizeMetric)
		]);

		result.Errors.Should().AllSatisfy(x =>
			x.ErrorMessage.Should().Match("The length of '*' must be * characters or fewer. You entered * characters.")
		);
	}

	[Fact]
	internal void Validate_With_MacrosBelowMinimum_Fails()
	{
		// Act
		var result = _validator.TestValidate(TestData.MacrosBelowMinimum);

		// Assert
		result.Errors.Should().HaveCount(8);

		result.Errors.Select(x => x.PropertyName).Should().BeEquivalentTo([
			nameof(CreateProductRequest.Calories),
			nameof(CreateProductRequest.TotalFat),
			nameof(CreateProductRequest.TotalCarbs),
			nameof(CreateProductRequest.Protein),
			nameof(CreateProductRequest.SaturatedFat),
			nameof(CreateProductRequest.TransFat),
			nameof(CreateProductRequest.Sugars),
			nameof(CreateProductRequest.Fiber),
		]);

		result.Errors.Should().AllSatisfy(x =>
			x.ErrorMessage.Should().Match("'*' must be greater than or equal to '0'.")
		);
	}

	[Fact]
	internal void Validate_With_MacrosAboveMaximum_Fails()
	{
		// Act
		var result = _validator.TestValidate(TestData.MacrosAboveMaximum);

		// Assert
		result.Errors.Should().HaveCount(8);

		result.Errors.Select(x => x.PropertyName).Should().BeEquivalentTo([
			nameof(CreateProductRequest.Calories),
			nameof(CreateProductRequest.TotalFat),
			nameof(CreateProductRequest.TotalCarbs),
			nameof(CreateProductRequest.Protein),
			nameof(CreateProductRequest.SaturatedFat),
			nameof(CreateProductRequest.TransFat),
			nameof(CreateProductRequest.Sugars),
			nameof(CreateProductRequest.Fiber),
		]);

		result.Errors.Should().AllSatisfy(x =>
			x.ErrorMessage.Should().Match("'*' must be less than '*'.")
		);
	}
}

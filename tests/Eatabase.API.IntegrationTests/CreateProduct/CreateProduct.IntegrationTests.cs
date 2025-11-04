using System.Net;
using Eatabase.API.Features.Products;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Eatabase.API.IntegrationTests.CreateProduct;

using TestData = CreateProductIntegrationTestsData;

public sealed class CreateProductTests(
	InMemoryDbWebApplicationFactory factory
) : IClassFixture<InMemoryDbWebApplicationFactory>
{
	private readonly CreateProductIntegrationTestsHelpers _helpers = new (factory);

	[Theory]
	[MemberData(nameof(TestData.ValidRequests), MemberType = typeof(TestData))]
	internal async Task CreateProduct_With_ValidRequest_Returns_Created_And_Persists(CreateProductRequest request)
	{
		// Act
		var (response, result) = await _helpers.CreateProductWithResult(request);

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.Created);
		response.Headers.Location.Should().NotBeNull().And.Be($"/products/{result}");

		result.Should().NotBe(Guid.Empty);

		var product = await _helpers.FindProduct(result);

		product.Should().NotBeNull();
		product.Id.Should().Be(result);
		product.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
		product.UpdatedAt.Should().BeNull();
		product.Brand.Should().Be(request.Brand);
		product.Name.Should().Be(request.Name);
		product.ServingSize.Should().Be(request.ServingSize);
		product.ServingSizeMetric.Should().Be(request.ServingSizeMetric);
		product.Calories.Should().Be(request.Calories);
		product.TotalFat.Should().Be(request.TotalFat);
		product.SaturatedFat.Should().Be(request.SaturatedFat);
		product.TransFat.Should().Be(request.TransFat);
		product.TotalCarbs.Should().Be(request.TotalCarbs);
		product.Sugars.Should().Be(request.Sugars);
		product.Fiber.Should().Be(request.Fiber);
		product.Protein.Should().Be(request.Protein);
	}

	[Fact]
	internal async Task CreateProduct_With_DuplicateBrandAndName_Returns_Conflict()
	{
		// Arrange
		var request = TestData.DuplicateBrandAndName;

		// Act
		var (response1, result1) = await _helpers.CreateProductWithResult(request);
		var response2 = await _helpers.CreateProduct(request);

		// Assert
		response1.StatusCode.Should().Be(HttpStatusCode.Created);
		response2.StatusCode.Should().Be(HttpStatusCode.Conflict);

		var products = await _helpers.WithDbContext(async dbContext =>
			await dbContext.Products.Where(p => p.Brand == request.Brand && p.Name == request.Name).ToListAsync()
		);

		products.Should().ContainSingle();
		products.Single().Id.Should().Be(result1);
	}

	[Theory]
	[MemberData(nameof(TestData.DifferentBrandNameCombination), MemberType = typeof(TestData))]
	internal async Task CreateProduct_With_DifferentBrandNameCombination_Returns_Created(
		CreateProductRequest request1,
		CreateProductRequest request2
	)
	{
		// Act
		var response1 = await _helpers.CreateProduct(request1);
		var response2 = await _helpers.CreateProduct(request2);

		// Assert
		response1.StatusCode.Should().Be(HttpStatusCode.Created);
		response2.StatusCode.Should().Be(HttpStatusCode.Created);
	}

	[Fact]
	internal async Task CreateProduct_WithInvalidRequest_Returns_BadRequest_WithErrors()
	{
		// Act
		var (response, result) = await _helpers.CreateProductWithValidationErrors(TestData.InvalidRequest);

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

		result.Errors.Should()
			.HaveCount(7).And
			.ContainKeys([
				nameof(CreateProductRequest.Brand),
				nameof(CreateProductRequest.Name),
				nameof(CreateProductRequest.ServingSize),
				nameof(CreateProductRequest.ServingSizeMetric),
				nameof(CreateProductRequest.Calories),
				nameof(CreateProductRequest.TotalFat),
				nameof(CreateProductRequest.TotalCarbs)
			]);
	}
}

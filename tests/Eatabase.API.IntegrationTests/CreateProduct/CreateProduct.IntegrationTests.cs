using System.Net;
using System.Net.Http.Json;
using Eatabase.API.Data;
using Eatabase.API.Features.Products;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Eatabase.API.IntegrationTests.CreateProduct;

using TestData = CreateProductIntegrationTestsData;

public sealed class CreateProductTests(
	InMemoryDbWebApplicationFactory factory
) : IClassFixture<InMemoryDbWebApplicationFactory>
{
	private readonly HttpClient _apiClient = factory.CreateClient();

	private async Task<(HttpResponseMessage, Guid)> CreateProduct(CreateProductRequest request)
	{
		var response = await _apiClient.PostAsJsonAsync("/products", request);
		var result = await response.Content.ReadFromJsonAsync<Guid>();

		return (response, result);
	}

	private async Task<Product?> FindProduct(Guid productId)
	{
		using var scope = factory.Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

		return await dbContext.Products.FindAsync(productId);
	}

	[Theory]
	[MemberData(nameof(TestData.ValidRequests), MemberType = typeof(TestData))]
	internal async Task CreateProduct_With_ValidRequest_Returns_Created_And_Persists(CreateProductRequest request)
	{
		// Act
		var (response, result) = await CreateProduct(request);

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.Created);
		response.Headers.Location.Should().NotBeNull().And.Be($"/products/{result}");

		result.Should().NotBe(Guid.Empty);

		var product = await FindProduct(result);

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
}

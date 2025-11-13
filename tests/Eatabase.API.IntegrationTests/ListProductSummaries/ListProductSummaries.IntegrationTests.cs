using System.Net;
using FluentAssertions;

namespace Eatabase.API.IntegrationTests.ListProductSummaries;

public sealed class ListProductSummariesTests(
	InMemoryDbWebApplicationFactory factory
) : IClassFixture<InMemoryDbWebApplicationFactory>
{
	private readonly ListProductSummariesIntegrationTestsHelpers _helpers = new (factory);

	[Fact]
	internal async Task ListProductSummaries_When_NoProducts_Returns_Ok_With_EmptyList()
	{
		// Arrange
		await _helpers.ClearProducts();

		// Act
		var (response, result) = await _helpers.ListProductsWithResult();

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.OK);

		result.Should().NotBeNull().And.BeEmpty();
	}

	[Fact]
	internal async Task ListProductSummaries_When_ProductsExist_Returns_Ok_With_ProductSummaries()
	{
		// Arrange
		var products = ListProductSummariesIntegrationTestsData.Products;
		await _helpers.InsertProducts(products);

		// Act
		var (response, result) = await _helpers.ListProductsWithResult();

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.OK);

		result.Should().NotBeNull().And.HaveCount(3);

		foreach (var product in result)
			result.Should().ContainSingle(x =>
				x.Id == product.Id
			);
	}
}

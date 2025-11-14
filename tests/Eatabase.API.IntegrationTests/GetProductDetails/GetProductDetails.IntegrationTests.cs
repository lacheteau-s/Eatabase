using System.Net;
using FluentAssertions;

namespace Eatabase.API.IntegrationTests.GetProductDetails;

public class GetProductDetailsTests(
	InMemoryDbWebApplicationFactory factory
) : IClassFixture<InMemoryDbWebApplicationFactory>
{
	private readonly GetProductDetailsIntegrationTestsHelpers _helpers = new (factory);

	[Fact]
	internal async Task GetProductDetails_When_IdDoesNotExist_Returns_NotFound()
	{
		// Act
		var response = await _helpers.GetProductDetails(Guid.NewGuid());

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.NotFound);
	}

	[Fact]
	internal async Task GetProductDetails_When_IdExists_Returns_Ok_With_ProductDetails()
	{
		// Arrange
		var product = GetProductDetailsIntegrationTestsData.Product;
		await _helpers.InsertProduct(product);

		// Act
		var (response, result) = await _helpers.GetProductDetailsWithResult(product.Id);

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.OK);
		result.Should().NotBeNull();
		result.Id.Should().Be(product.Id);
	}
}

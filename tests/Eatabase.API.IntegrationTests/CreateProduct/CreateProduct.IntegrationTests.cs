using System.Net;
using System.Net.Http.Json;
using Eatabase.API.Features.Products;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Eatabase.API.IntegrationTests.CreateProduct;

using TestData = CreateProductIntegrationTestsData;

public sealed class CreateProductTests(
	WebApplicationFactory<Program> factory
) : IClassFixture<WebApplicationFactory<Program>>
{
	private readonly HttpClient _apiClient = factory.CreateClient();

	private async Task<(HttpResponseMessage, Guid)> CreateProduct(CreateProductRequest request)
	{
		var response = await _apiClient.PostAsJsonAsync("/products", request);
		var result = await response.Content.ReadFromJsonAsync<Guid>();

		return (response, result);
	}

	[Theory]
	[MemberData(nameof(TestData.ValidRequests), MemberType = typeof(TestData))]
	internal async Task CreateProduct_With_ValidRequest_Returns_Created_With_Guid(CreateProductRequest request)
	{
		// Act
		var (response, result) = await CreateProduct(request);

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.Created);
		response.Headers.Location.Should().NotBeNull();
		response.Headers.Location.Should().Be($"/products/{result}");

		result.Should().NotBe(Guid.Empty);
	}
}

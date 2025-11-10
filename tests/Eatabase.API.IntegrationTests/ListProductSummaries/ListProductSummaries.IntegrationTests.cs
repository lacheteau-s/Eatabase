using System.Net;
using System.Net.Http.Json;
using Eatabase.API.Features.Products;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Eatabase.API.IntegrationTests.ListProductSummaries;

public sealed class ListProductSummariesTests(
	WebApplicationFactory<Program> factory
) : IClassFixture<WebApplicationFactory<Program>>
{
	private readonly HttpClient _client = factory.CreateClient();

	[Fact]
	internal async Task ListProductSummaries_When_NoProducts_Returns_Ok_With_EmptyList()
	{
		// Act
		var response = await _client.GetAsync("/products");
		var result = await response.Content.ReadFromJsonAsync<List<ProductSummary>>();

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.OK);

		result.Should().NotBeNull().And.BeEmpty();
	}
}

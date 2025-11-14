using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Eatabase.API.IntegrationTests.GetProductDetails;

public class GetProductDetailsTests(
	WebApplicationFactory<Program> factory
) : IClassFixture<WebApplicationFactory<Program>>
{
	private readonly HttpClient _client = factory.CreateClient();

	[Fact]
	internal async Task GetProductDetails_Returns_NotFound()
	{
		// Act
		var response = await _client.GetAsync($"/products/{Guid.NewGuid()}");

		// Assert
		response.StatusCode.Should().Be(HttpStatusCode.NotFound);
	}
}

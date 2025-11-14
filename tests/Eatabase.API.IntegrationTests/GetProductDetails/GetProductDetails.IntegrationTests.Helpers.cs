using System.Net.Http.Json;
using Eatabase.API.Data;
using Eatabase.API.Features.Products;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Eatabase.API.IntegrationTests.GetProductDetails;

internal class GetProductDetailsIntegrationTestsHelpers(WebApplicationFactory<Program> factory)
{
	private readonly HttpClient _client = factory.CreateClient();

	public Task<HttpResponseMessage> GetProductDetails(Guid productId) =>
		_client.GetAsync($"/products/{productId}");

	public async Task<(HttpResponseMessage, ProductDetails)> GetProductDetailsWithResult(Guid productId)
	{
		var response = await GetProductDetails(productId);
		var result = await response.Content.ReadFromJsonAsync<ProductDetails>();

		return (response, result!);
	}

	public async Task InsertProduct(Product product)
	{
		await WithDbContext(async dbContext =>
		{
			await dbContext.Products.AddAsync(product);
			await dbContext.SaveChangesAsync();
		});
	}

	private async Task WithDbContext(Func<AppDbContext, Task> action)
	{
		using var scope = factory.Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

		await action(dbContext);
	}
}

using System.Net.Http.Json;
using Eatabase.API.Data;
using Eatabase.API.Features.Products;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Eatabase.API.IntegrationTests.ListProductSummaries;

internal class ListProductSummariesIntegrationTestsHelpers(WebApplicationFactory<Program> factory)
{
	private readonly HttpClient _client = factory.CreateClient();

	public async Task<(HttpResponseMessage, List<ProductSummary>)> ListProductsWithResult()
	{
		var response = await _client.GetAsync("/products");
		var result = await response.Content.ReadFromJsonAsync<List<ProductSummary>>();

		return (response, result!);
	}

	public async Task InsertProducts(params Product[] products)
	{
		await WithDbContext(async dbContext =>
		{
			await dbContext.Products.AddRangeAsync(products);
			await dbContext.SaveChangesAsync();
		});
	}

	public async Task ClearProducts()
	{
		await WithDbContext(async dbContext =>
		{
			dbContext.Products.RemoveRange(dbContext.Products);
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

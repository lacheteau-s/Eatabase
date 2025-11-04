using System.Net.Http.Json;
using System.Runtime.Serialization;
using Eatabase.API.Data;
using Eatabase.API.Features.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Eatabase.API.IntegrationTests.CreateProduct;

internal class CreateProductIntegrationTestsHelpers(WebApplicationFactory<Program> factory)
{
	private readonly HttpClient _apiClient = factory.CreateClient();

	public Task<HttpResponseMessage> CreateProduct(CreateProductRequest request) =>
		_apiClient.PostAsJsonAsync("/products", request);

	public async Task<(HttpResponseMessage, Guid)> CreateProductWithResult(CreateProductRequest request)
	{
		var response = await CreateProduct(request);
		var result = await response.Content.ReadFromJsonAsync<Guid>();

		return (response, result);
	}

	public async Task<(HttpResponseMessage, HttpValidationProblemDetails)> CreateProductWithValidationErrors(CreateProductRequest request)
	{
		var response = await CreateProduct(request);
		var result = await response.Content.ReadFromJsonAsync<HttpValidationProblemDetails>()
			?? throw new SerializationException("Could not deserialize validation problem details.");
		
		return (response, result);
	}

	public Task<Guid> ReadContent(HttpResponseMessage response) =>
		response.Content.ReadFromJsonAsync<Guid>();

	public async Task<T> WithDbContext<T>(Func<AppDbContext, Task<T>> action)
	{
		using var scope = factory.Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

		return await action(dbContext);
	}

	public async Task<Product?> FindProduct(Guid productId) =>
		await WithDbContext(async dbContext => await dbContext.Products.FindAsync(productId));
}

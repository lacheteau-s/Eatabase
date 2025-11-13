using Eatabase.API.Features.Products;

namespace Eatabase.API.IntegrationTests.ListProductSummaries;

internal static class ListProductSummariesIntegrationTestsData
{
	private static Product CreateProduct(string brand, string name) => new()
	{
		Brand = brand,
		Name = name,
		ServingSize = "10 oz",
		ServingSizeMetric = "300g",
		Calories = 123,
		TotalFat = 4.5m,
		SaturatedFat = 6.7m,
		TransFat = 8.9m,
		TotalCarbs = 8.7m,
		Sugars = 6.5m,
		Fiber = 4.3m,
		Protein = 2.1m
	};

	public static Product[] Products =>
	[
		CreateProduct("Brand A", "Product 1"),
		CreateProduct("Brand B", "Product 2"),
		CreateProduct("Brand C", "Product 3")
	];
}

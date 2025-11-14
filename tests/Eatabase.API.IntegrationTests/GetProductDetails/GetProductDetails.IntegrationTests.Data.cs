using Eatabase.API.Features.Products;

namespace Eatabase.API.IntegrationTests.GetProductDetails;

internal static class GetProductDetailsIntegrationTestsData
{
	private static Product CreateProduct(string brand, string name) => new()
	{
		Id = Guid.NewGuid(),
		CreatedAt = DateTime.UtcNow,
		UpdatedAt = null,
		Brand = brand,
		Name = name,
		ServingSize = "100",
		ServingSizeMetric = "g",
		Calories = 200,
		TotalFat = 10.0m,
		SaturatedFat = 3.0m,
		TransFat = 0.0m,
		TotalCarbs = 25.0m,
		Sugars = 5.0m,
		Fiber = 2.0m,
		Protein = 8.0m	
	};

	public static Product Product => CreateProduct("Test Brand", "Test Product");
}

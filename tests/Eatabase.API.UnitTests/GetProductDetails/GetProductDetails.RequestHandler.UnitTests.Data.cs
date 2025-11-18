using Eatabase.API.Features.Products;

namespace Eatabase.API.UnitTests.GetProductDetails;

internal class GetProductDetailsRequestHandlerTestsData
{
	private static Product CreateProduct(
		string brand, string name,
		DateTime? updatedAt = null,
		decimal? saturatedFat = null, decimal? transFat = null,
		decimal? sugars = null, decimal? fiber = null
	) => new()
	{
		Brand = brand,
		Name = name,
		CreatedAt = DateTime.UtcNow.AddDays(-1),
		UpdatedAt = updatedAt,
		ServingSize = "10 oz",
		ServingSizeMetric = "300g",
		Calories = 123,
		TotalFat = 4.5m,
		SaturatedFat = saturatedFat,
		TransFat = transFat,
		TotalCarbs = 8.7m,
		Sugars = sugars,
		Fiber = fiber,
		Protein = 2.1m	
	};

	public static TheoryData<Product> Products =>
	[
		CreateProduct(
			"Brand A", "Product 1",
			updatedAt: DateTime.UtcNow,
			saturatedFat: 6.7m, transFat: 8.9m,
			sugars: 6.5m, fiber: 4.3m
		),
		CreateProduct("Brand B", "Product 2"),
	];
}

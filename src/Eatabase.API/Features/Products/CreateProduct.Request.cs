namespace Eatabase.API.Features.Products;

internal sealed record CreateProductRequest(
	string Brand,
	string Name,
	string ServingSize,
	string ServingSizeMetric,
	int Calories,
	decimal TotalFat,
	decimal? SaturatedFat,
	decimal? TransFat,
	decimal TotalCarbs,
	decimal? Sugars,
	decimal? Fiber,
	decimal Protein
);

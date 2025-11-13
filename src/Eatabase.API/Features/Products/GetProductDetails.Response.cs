namespace Eatabase.API.Features.Products;

internal sealed record ProductDetails(
	Guid Id,
	DateTime CreatedAt,
	DateTime? UpdatedAt,
	string Brand,
	string Name,
	int Calories,
	decimal TotalFat,
	decimal? SaturatedFat,
	decimal? TransFat,
	decimal TotalCarbs,
	decimal? Sugars,
	decimal? Fiber,
	decimal Protein
);

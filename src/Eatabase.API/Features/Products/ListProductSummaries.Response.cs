namespace Eatabase.API.Features.Products;

internal sealed record ProductSummary(
	Guid Id,
	string Brand,
	string Name
);

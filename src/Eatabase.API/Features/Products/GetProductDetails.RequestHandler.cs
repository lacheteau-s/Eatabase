using Eatabase.API.Data;

namespace Eatabase.API.Features.Products;

internal sealed class GetProductDetailsRequestHandler(AppDbContext dbContext)
{
	public async Task<ProductDetails?> HandleAsync(Guid id, CancellationToken ct)
	{
		var product = await dbContext.Products.FindAsync([id], ct);

		if (product is null)
			return null;

		return new ProductDetails(
			product.Id,
			product.CreatedAt,
			product.UpdatedAt,
			product.Brand,
			product.Name,
			product.Calories,
			product.TotalFat,
			product.SaturatedFat,
			product.TransFat,
			product.TotalCarbs,
			product.Sugars,
			product.Fiber,
			product.Protein
		);
	}
}

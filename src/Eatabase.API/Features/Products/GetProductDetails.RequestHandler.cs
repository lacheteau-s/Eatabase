using Eatabase.API.Data;
using Eatabase.API.Results;

namespace Eatabase.API.Features.Products;

internal sealed class GetProductDetailsRequestHandler(AppDbContext dbContext)
{
	public async Task<Result<ProductDetails>> HandleAsync(Guid id, CancellationToken ct)
	{
		var product = await dbContext.Products.FindAsync([id], ct);

		if (product is null)
			return Result.Failure<ProductDetails>();

		return Result.Success(new ProductDetails(
			product.Id,
			product.CreatedAt,
			product.UpdatedAt,
			product.Brand,
			product.Name,
			product.ServingSize,
			product.ServingSizeMetric,
			product.Calories,
			product.TotalFat,
			product.SaturatedFat,
			product.TransFat,
			product.TotalCarbs,
			product.Sugars,
			product.Fiber,
			product.Protein
		));
	}
}

using Eatabase.API.Data;

namespace Eatabase.API.Features.Products;

internal class CreateProductRequestHandler(AppDbContext dbContext)
{
	public async Task<Guid> HandleAsync(CreateProductRequest request, CancellationToken ct)
	{
		var product = new Product
		{
			Brand = request.Brand,
			Name = request.Name,
			ServingSize = request.ServingSize,
			ServingSizeMetric = request.ServingSizeMetric,
			Calories = request.Calories,
			TotalFat = request.TotalFat,
			SaturatedFat = request.SaturatedFat,
			TransFat = request.TransFat,
			TotalCarbs = request.TotalCarbs,
			Sugars = request.Sugars,
			Fiber = request.Fiber,
			Protein = request.Protein,
			CreatedAt = DateTime.UtcNow
		};

		dbContext.Products.Add(product);

		await dbContext.SaveChangesAsync(ct);

		return product.Id;
	}
}

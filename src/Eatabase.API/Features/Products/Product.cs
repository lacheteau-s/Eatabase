namespace Eatabase.API.Features.Products;

internal sealed class Product
{
	public Guid Id { get; set; }

	public DateTime CreatedAt { get; set; }

	public DateTime? UpdatedAt { get; set; }

	public required string Brand { get; set; }

	public required string Name { get; set; }

	public required string ServingSize { get; set; }

	public required string ServingSizeMetric { get; set; }

	public int Calories { get; set; }

	public decimal TotalFat { get; set; }

	public decimal? SaturatedFat { get; set; }

	public decimal? TransFat { get; set; }

	public decimal TotalCarbs { get; set; }
	
	public decimal? Sugars { get; set; }

	public decimal? Fiber { get; set; }

	public decimal Protein { get; set; }
}

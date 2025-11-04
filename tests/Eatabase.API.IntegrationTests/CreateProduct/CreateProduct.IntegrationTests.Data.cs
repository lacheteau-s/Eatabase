using Eatabase.API.Features.Products;

namespace Eatabase.API.IntegrationTests.CreateProduct;

internal static class CreateProductIntegrationTestsData
{
	public static CreateProductRequest BaseRequest => new(
		Brand: "Test Brand",
		Name: "Test Name",
		ServingSize: "10 oz",
		ServingSizeMetric: "300 g",
		Calories: 123,
		TotalFat: 4.5m,
		SaturatedFat: 6.7m,
		TransFat: 8.9m,
		TotalCarbs: 8.7m,
		Sugars: 6.5m,
		Fiber: 4.3m,
		Protein: 2.1m
	);

	public static CreateProductRequest WithoutOptionalFields => BaseRequest with
	{
		SaturatedFat = null,
		TransFat = null,
		Sugars = null,
		Fiber = null
	};

	public static TheoryData<CreateProductRequest> ValidRequests =>
	[
		BaseRequest with { Name = "Valid Request 1" },
		WithoutOptionalFields with { Name = "Valid Request 2" }
	];

	public static CreateProductRequest DuplicateBrandAndName => BaseRequest with
	{
		Brand = "Duplicate Brand",
		Name = "Duplicate Name"
	};

	public static TheoryData<CreateProductRequest, CreateProductRequest> DifferentBrandNameCombination => new()
	{
		// Same Brand different Name
		{
			BaseRequest with { Brand = "Same Brand", Name = "Name 1" },
			BaseRequest with { Brand = "Same Brand", Name = "Name 2" }
		},
		// Same Name different Brand
		{
			BaseRequest with { Brand = "Brand 1", Name = "Same Name" },
			BaseRequest with { Brand = "Brand 2", Name = "Same Name" }
		}
	};

	public static CreateProductRequest InvalidRequest => BaseRequest with
	{
		Brand = null!,
		Name = string.Empty,
		ServingSize = new string(' ', 25),
		ServingSizeMetric = new string('A', 26),
		Calories = 1000,
		TotalFat = 100,
		TotalCarbs = -0.01m
	};
}

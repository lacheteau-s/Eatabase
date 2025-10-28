using Eatabase.API.Features.Products;

namespace Eatabase.API.IntegrationTests.CreateProduct;

internal static class CreateProductIntegrationTestsData
{
	private static CreateProductRequest BaseRequest => new(
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

	private static CreateProductRequest BaseRequestWithNulls => BaseRequest with
	{
		SaturatedFat = null,
		TransFat = null,
		Sugars = null,
		Fiber = null
	};

	public static TheoryData<CreateProductRequest> ValidRequests =>
	[
		BaseRequest,
		BaseRequestWithNulls
	];
}

using Eatabase.API.Features.Products;

namespace Eatabase.API.UnitTests.CreateProduct;

using SharedData = CreateProductTestData;

internal static class CreateProductRequestHandlerTestsData
{
	public static TheoryData<CreateProductRequest> Requests =>
	[
		SharedData.BaseRequest,
		SharedData.WithoutOptionalFields
	];

	public static (CreateProductRequest, CreateProductRequest) MultipleRequests =>
	(
		SharedData.BaseRequest with { Name = "Request 1" },
		SharedData.WithoutOptionalFields with { Name = "Request 2" }
	);

	public static TheoryData<CreateProductRequest, CreateProductRequest> DifferentBrandNameCombination => new()
	{
		// Same Brand different Name
		{
			SharedData.BaseRequest with { Name = "Name 1" },
			SharedData.BaseRequest with { Name = "Name 2" }
		},
		// Same Name different Brand
		{
			SharedData.BaseRequest with { Brand = "Brand 1" },
			SharedData.BaseRequest with { Brand = "Brand 2" }
		}
	};
}

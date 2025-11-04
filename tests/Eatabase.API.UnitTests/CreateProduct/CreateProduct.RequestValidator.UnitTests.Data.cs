using Eatabase.API.Features.Products;

namespace Eatabase.API.UnitTests.CreateProduct;

using SharedData = CreateProductTestData;

internal static class CreateProductRequestValidatorTestData
{
	public static TheoryData<CreateProductRequest> ValidRequests =>
	[
		// Regular request
		SharedData.BaseRequest,
		// Regular request with null fields
		SharedData.WithoutOptionalFields,
		// Minimum bounds
		SharedData.BaseRequest with
		{
			Brand = "A",
			Name = "B",
			ServingSize = "C",
			ServingSizeMetric = "D",
			Calories = 0,
			TotalFat = 0m,
			SaturatedFat = 0m,
			TransFat = 0m,
			TotalCarbs = 0m,
			Sugars = 0m,
			Fiber = 0m,
			Protein = 0m
		},
		// Maximum bounds
		SharedData.BaseRequest with
		{
			Brand = new string('A', 50),
			Name = new string('B', 100),
			ServingSize = new string('C', 25),
			ServingSizeMetric = new string('D', 25),
			Calories = 999,
			TotalFat = 99.99m,
			SaturatedFat = 99.99m,
			TransFat = 99.99m,
			TotalCarbs = 99.99m,
			Sugars = 99.99m,
			Fiber = 99.99m,
			Protein = 99.99m
		}
	];

	public static TheoryData<CreateProductRequest> StringFieldsNullOrEmpty =>
	[
		// Null
		SharedData.BaseRequest with
		{
			Brand = null!,
			Name = null!,
			ServingSize = null!,
			ServingSizeMetric = null!
		},
		// Empty
		SharedData.BaseRequest with
		{
			Brand = string.Empty,
			Name = string.Empty,
			ServingSize = string.Empty,
			ServingSizeMetric = string.Empty
		},
		// Whitespace
		SharedData.BaseRequest with
		{
			Brand = new string(' ', 50),
			Name = new string(' ', 100),
			ServingSize = new string(' ', 25),
			ServingSizeMetric = new string(' ', 25)
		}
	];

	public static CreateProductRequest StringFieldsTooLong => SharedData.BaseRequest with
	{
		Brand = new string('A', 51),
		Name = new string('B', 101),
		ServingSize = new string('C', 26),
		ServingSizeMetric = new string('D', 26)
	};

	public static CreateProductRequest MacrosBelowMinimum => SharedData.BaseRequest with
	{
		Calories = -1,
		TotalFat = -0.01m,
		SaturatedFat = -0.01m,
		TransFat = -0.01m,
		TotalCarbs = -0.01m,
		Sugars = -0.01m,
		Fiber = -0.01m,
		Protein = -0.01m
	};

	public static CreateProductRequest MacrosAboveMaximum => SharedData.BaseRequest with
	{
		Calories = 1000,
		TotalFat = 100m,
		SaturatedFat = 100m,
		TransFat = 100m,
		TotalCarbs = 100m,
		Sugars = 100m,
		Fiber = 100m,
		Protein = 100m
	};
}

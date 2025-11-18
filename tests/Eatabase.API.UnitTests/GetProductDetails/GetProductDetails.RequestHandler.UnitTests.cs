using Eatabase.API.Data;
using Eatabase.API.Features.Products;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Eatabase.API.UnitTests.GetProductDetails;

using TestData = GetProductDetailsRequestHandlerTestsData;

public class GetProductDetailsRequestHandlerTests
{
	private readonly GetProductDetailsRequestHandler _handler;

	private readonly AppDbContext _dbContext;

	public GetProductDetailsRequestHandlerTests()
	{
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		_dbContext = new AppDbContext(options);
		_handler = new (_dbContext);
	}

	[Fact]
	internal async Task Handle_When_IdDoesNotExist_Returns_Null()
	{
		var result = await _handler.HandleAsync(Guid.NewGuid(), default);

		result.Should().BeNull();
	}

	[Theory]
	[MemberData(nameof(TestData.Products), MemberType = typeof(TestData))]
	internal async Task Handle_When_IdExists_Returns_ProductDetails(Product product)
	{
		// Arrange
		_dbContext.Products.Add(product);
		await _dbContext.SaveChangesAsync();

		// Act
		var result = await _handler.HandleAsync(product.Id, default);

		// Assert
		result.Should().NotBeNull();
		result.Id.Should().Be(product.Id);
		result.CreatedAt.Should().Be(product.CreatedAt);
		result.UpdatedAt.Should().Be(product.UpdatedAt);
		result.Brand.Should().Be(product.Brand);
		result.Name.Should().Be(product.Name);
		result.ServingSize.Should().Be(product.ServingSize);
		result.ServingSizeMetric.Should().Be(product.ServingSizeMetric);
		result.Calories.Should().Be(product.Calories);
		result.TotalFat.Should().Be(product.TotalFat);
		result.SaturatedFat.Should().Be(product.SaturatedFat);
		result.TransFat.Should().Be(product.TransFat);
		result.TotalCarbs.Should().Be(product.TotalCarbs);
		result.Sugars.Should().Be(product.Sugars);
		result.Fiber.Should().Be(product.Fiber);
		result.Protein.Should().Be(product.Protein);
	}
}

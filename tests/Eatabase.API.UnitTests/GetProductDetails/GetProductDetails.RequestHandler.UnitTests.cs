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
	internal async Task Handle_When_IdDoesNotExist_Returns_Failure()
	{
		var result = await _handler.HandleAsync(Guid.NewGuid(), default);

		result.IsFailure.Should().BeTrue();
		// result.Should().BeNull();
	}

	[Theory]
	[MemberData(nameof(TestData.Products), MemberType = typeof(TestData))]
	internal async Task Handle_When_IdExists_Returns_Success_With_ProductDetails(Product product)
	{
		// Arrange
		_dbContext.Products.Add(product);
		await _dbContext.SaveChangesAsync();

		// Act
		var result = await _handler.HandleAsync(product.Id, default);

		// Assert
		result.IsSuccess.Should().BeTrue();
		result.Value.Should().NotBeNull();

		var details = result.Value;

		details.Id.Should().Be(product.Id);
		details.CreatedAt.Should().Be(product.CreatedAt);
		details.UpdatedAt.Should().Be(product.UpdatedAt);
		details.Brand.Should().Be(product.Brand);
		details.Name.Should().Be(product.Name);
		details.ServingSize.Should().Be(product.ServingSize);
		details.ServingSizeMetric.Should().Be(product.ServingSizeMetric);
		details.Calories.Should().Be(product.Calories);
		details.TotalFat.Should().Be(product.TotalFat);
		details.SaturatedFat.Should().Be(product.SaturatedFat);
		details.TransFat.Should().Be(product.TransFat);
		details.TotalCarbs.Should().Be(product.TotalCarbs);
		details.Sugars.Should().Be(product.Sugars);
		details.Fiber.Should().Be(product.Fiber);
		details.Protein.Should().Be(product.Protein);
	}
}

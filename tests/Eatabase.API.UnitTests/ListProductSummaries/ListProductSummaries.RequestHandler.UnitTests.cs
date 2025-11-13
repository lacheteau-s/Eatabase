using Eatabase.API.Data;
using Eatabase.API.Features.Products;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Eatabase.API.UnitTests.ListProductSummaries;

public sealed class ListProductSummariesRequestHandlerTests
{
	private readonly ListProductSummariesRequestHandler _handler;

	private readonly AppDbContext _dbContext;

	public ListProductSummariesRequestHandlerTests()
	{
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		_dbContext = new AppDbContext(options);
		_handler = new (_dbContext);
	}

	[Fact]
	internal async Task Handle_When_NoProducts_Returns_EmptyList()
	{
		// Act
		var summaries = await _handler.HandleAsync(default);

		// Assert
		summaries.Should().NotBeNull().And.BeEmpty();
	}

	[Fact]
	internal async Task Handle_When_ProductsExist_Returns_ProductSummaries()
	{
		// Arrange
		var products = ListProductSummariesRequestHandlerTestsData.Products;

		await _dbContext.Products.AddRangeAsync(products);
		await _dbContext.SaveChangesAsync();

		// Act
		var summaries = await _handler.HandleAsync(default);

		// Assert
		summaries.Should().NotBeNull().And.HaveCount(products.Length);

		foreach (var product in products)
		{
			product.Id.Should().NotBe(Guid.Empty);
			summaries.Should().ContainSingle(x =>
				x.Brand == product.Brand &&
				x.Name == product.Name
			);
		}
	}
}

using Eatabase.API.Data;
using Eatabase.API.Features.Products;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Eatabase.API.UnitTests.CreateProduct;

using SharedTestData = CreateProductTestData;
using TestData = CreateProductRequestHandlerTestsData;

public sealed class CreateProductRequestHandlerTests
{
	private readonly CreateProductRequestHandler _handler;

	public CreateProductRequestHandlerTests()
	{
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		var dbContext = new AppDbContext(options);
		_handler = new (dbContext);
	}

	[Theory]
	[MemberData(nameof(TestData.Requests), MemberType = typeof(TestData))]
	internal async Task Handle_With_Request_Returns_Guid(CreateProductRequest request)
	{
		// Act
		var id = await _handler.HandleAsync(request, default);

		// Assert
		id.Should().NotBeNull().And.NotBe(Guid.Empty);
	}

	[Fact]
	internal async Task Handle_With_MultipleRequests_Returns_UniqueIds()
	{
		// Arrange
		var (request1, request2) = TestData.MultipleRequests;

		// Act
		var id1 = await _handler.HandleAsync(request1, default);
		var id2 = await _handler.HandleAsync(request2, default);

		// Assert
		id1.Should().NotBeNull().And.NotBe(Guid.Empty);
		id2.Should().NotBeNull().And.NotBe(Guid.Empty);

		id1.Value.Should().NotBe(id2.Value);
	}

	[Fact]
	internal async Task Handle_With_DuplicateBrandAndName_Returns_Null()
	{
		// Act
		var original = await _handler.HandleAsync(SharedTestData.BaseRequest, default);
		var duplicate = await _handler.HandleAsync(SharedTestData.BaseRequest, default);

		// Assert
		original.Should().NotBeNull().And.NotBe(Guid.Empty);

		duplicate.Should().BeNull();
	}

	[Theory]
	[MemberData(nameof(TestData.DifferentBrandNameCombination), MemberType = typeof(TestData))]
	internal async Task Handle_With_DifferentBrandNameCombination_Returns_Guid(
		CreateProductRequest request1,
		CreateProductRequest request2
	)
	{
		// Act
		var product1 = await _handler.HandleAsync(request1, default);
		var product2 = await _handler.HandleAsync(request2, default);

		// Assert
		product1.Should().NotBeNull().And.NotBe(Guid.Empty);
		product2.Should().NotBeNull().And.NotBe(Guid.Empty);

		product1.Value.Should().NotBe(product2.Value);
	}
}

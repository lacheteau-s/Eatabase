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
	internal async Task Handle_With_Request_Returns_Success_With_Guid(CreateProductRequest request)
	{
		// Act
		var result = await _handler.HandleAsync(request, default);

		// Assert
		result.IsSuccess.Should().BeTrue();
		result.Value.Should().NotBe(Guid.Empty);
		// id.Should().NotBeNull().And.NotBe(Guid.Empty);
	}

	[Fact]
	internal async Task Handle_With_MultipleRequests_Returns_Success_With_UniqueIds()
	{
		// Arrange
		var (request1, request2) = TestData.MultipleRequests;

		// Act
		var result1 = await _handler.HandleAsync(request1, default);
		var result2 = await _handler.HandleAsync(request2, default);

		// Assert
		result1.IsSuccess.Should().BeTrue();
		result1.Value.Should().NotBe(Guid.Empty);
		// id1.Should().NotBeNull().And.NotBe(Guid.Empty);
		result2.IsSuccess.Should().BeTrue();
		result2.Value.Should().NotBe(Guid.Empty);
		// id2.Should().NotBeNull().And.NotBe(Guid.Empty);

		result1.Value.Should().NotBe(result2.Value);
	}

	[Fact]
	internal async Task Handle_With_DuplicateBrandAndName_Returns_Failure()
	{
		// Act
		var original = await _handler.HandleAsync(SharedTestData.BaseRequest, default);
		var duplicate = await _handler.HandleAsync(SharedTestData.BaseRequest, default);

		// Assert
		original.IsSuccess.Should().BeTrue();
		original.Value.Should().NotBe(Guid.Empty);
		// original.Should().NotBeNull().And.NotBe(Guid.Empty);

		duplicate.IsFailure.Should().BeTrue();
		// duplicate.Should().BeNull();
	}

	[Theory]
	[MemberData(nameof(TestData.DifferentBrandNameCombination), MemberType = typeof(TestData))]
	internal async Task Handle_With_DifferentBrandNameCombination_Returns_Success_With_Guid(
		CreateProductRequest request1,
		CreateProductRequest request2
	)
	{
		// Act
		var result1 = await _handler.HandleAsync(request1, default);
		var result2 = await _handler.HandleAsync(request2, default);

		// Assert
		result1.IsSuccess.Should().BeTrue();
		result1.Value.Should().NotBe(Guid.Empty);
		// product1.Should().NotBeNull().And.NotBe(Guid.Empty);
		result2.IsSuccess.Should().BeTrue();
		result2.Value.Should().NotBe(Guid.Empty);
		// product2.Should().NotBeNull().And.NotBe(Guid.Empty);

		result1.Value.Should().NotBe(result2.Value);
	}
}

using Eatabase.API.Data;
using Eatabase.API.Features.Products;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Eatabase.API.UnitTests.CreateProduct;

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
		id.Should().NotBe(Guid.Empty);
	}

	[Fact]
	internal async Task Handle_With_MultipleRequests_Returns_UniqueIds()
	{
		// Act
		var id1 = await _handler.HandleAsync(TestData.BaseRequest, default);
		var id2 = await _handler.HandleAsync(TestData.BaseRequestWithNulls, default);

		// Assert
		id1.Should().NotBe(Guid.Empty);
		id2.Should().NotBe(Guid.Empty);

		id1.Should().NotBe(id2);
	}
}

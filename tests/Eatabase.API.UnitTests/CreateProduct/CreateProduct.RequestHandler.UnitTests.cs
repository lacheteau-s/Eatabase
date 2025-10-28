using Eatabase.API.Features.Products;
using FluentAssertions;

namespace Eatabase.API.UnitTests.CreateProduct;

using TestData = CreateProductRequestHandlerTestsData;

public sealed class CreateProductRequestHandlerTests
{
	private readonly CreateProductRequestHandler _handler = new();

	[Theory]
	[MemberData(nameof(TestData.Requests), MemberType = typeof(TestData))]
	internal void Handle_With_Request_Returns_Guid(CreateProductRequest request)
	{
		// Act
		var id = _handler.Handle(request);

		// Assert
		id.Should().NotBe(Guid.Empty);
	}

	[Fact]
	internal void Handle_With_MultipleRequests_Returns_UniqueIds()
	{
		// Act
		var id1 = _handler.Handle(TestData.BaseRequest);
		var id2 = _handler.Handle(TestData.BaseRequestWithNulls);

		// Assert
		id1.Should().NotBe(Guid.Empty);
		id2.Should().NotBe(Guid.Empty);

		id1.Should().NotBe(id2);
	}
}

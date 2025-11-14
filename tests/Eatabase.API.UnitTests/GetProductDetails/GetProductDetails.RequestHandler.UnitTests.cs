using Eatabase.API.Features.Products;
using FluentAssertions;

namespace Eatabase.API.UnitTests.GetProductDetails;

public class GetProductDetailsRequestHandlerTests
{
	private readonly GetProductDetailsRequestHandler _handler = new();

	[Fact]
	internal void Handle_Returns_Null()
	{
		var result = _handler.Handle(Guid.NewGuid());

		result.Should().BeNull();
	}
}

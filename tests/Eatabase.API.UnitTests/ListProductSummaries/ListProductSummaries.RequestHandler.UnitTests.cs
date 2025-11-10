using Eatabase.API.Features.Products;
using FluentAssertions;

namespace Eatabase.API.UnitTests.ListProductSummaries;

public sealed class ListProductSummariesRequestHandlerTests
{
	private readonly ListProductSummariesRequestHandler _handler = new();

	[Fact]
	internal void Handle_Returns_EmptyList()
	{
		// Act
		var summaries = _handler.Handle();

		// Assert
		summaries.Should().NotBeNull().And.BeEmpty();
	}
}

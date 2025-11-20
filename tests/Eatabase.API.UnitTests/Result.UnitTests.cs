using Eatabase.API.Results;
using FluentAssertions;

namespace Eatabase.API.UnitTests;

public class ResultTests
{
	[Fact]
	internal void Success_IsSuccess()
	{
		// Act
		var result = Result.Success();

		// Assert
		result.IsSuccess.Should().BeTrue();
		result.IsFailure.Should().BeFalse();
	}

	[Fact]
	internal void Failure_IsFailure()
	{
		// Act
		var result = Result.Failure();

		// Assert
		result.IsSuccess.Should().BeFalse();
		result.IsFailure.Should().BeTrue();
	}

	[Theory]
	[InlineData(42)]
	[InlineData("Test")]
	internal void TypedSuccess_With_Value_IsSuccess_With_Value<T>(T value)
	{
		// Act
		var result = Result.Success(value);

		// Assert
		result.IsSuccess.Should().BeTrue();
		result.IsFailure.Should().BeFalse();
		result.Value.Should().Be(value);
	}

	[Fact]
	internal void TypedSuccess_With_Null_Throws()
	{
		// Act
		var result = () => Result.Success<object?>(null);

		// Assert
		result.Should().Throw<ArgumentNullException>().WithMessage("Success result must have a value. (Parameter 'value')");
	}

	[Fact]
	internal void TypedFailure_IsFailure()
	{
		// Act
		var result = Result.Failure<object>();

		// Assert
		result.IsSuccess.Should().BeFalse();
		result.IsFailure.Should().BeTrue();
	}

	[Fact]
	internal void TypedFailure_Value_Throws()
	{
		// Arrange
		var result = Result.Failure<object>();

		// Act
		var value = () => result.Value;

		// Assert
		value.Should().Throw<InvalidOperationException>().WithMessage("Cannot access value of a failed result.");
	}
}

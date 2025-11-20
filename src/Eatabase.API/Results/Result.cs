namespace Eatabase.API.Results;

internal class Result
{
	public bool IsSuccess { get; }
	public bool IsFailure => !IsSuccess;

	internal Result(bool isSuccess)
	{
		IsSuccess = isSuccess;
	}

	public static Result Success() => new(true);
	public static Result Failure() => new(false);

	public static Result<T> Success<T>(T value) => new(value, true);
	public static Result<T> Failure<T>() => new(default, false);
}

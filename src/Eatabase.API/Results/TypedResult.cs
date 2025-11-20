namespace Eatabase.API.Results;

internal class Result<T> : Result
{
	private readonly T? _value;

	public T Value => IsSuccess
		? _value!
		: throw new InvalidOperationException("Cannot access value of a failed result.");

	internal Result(T? value, bool isSuccess) : base(isSuccess)
	{
		if (isSuccess)
		{
			if (value is null)
				throw new ArgumentNullException(nameof(value), "Success result must have a value.");
	
			_value = value;
		}
	}
}

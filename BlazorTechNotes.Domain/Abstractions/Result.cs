namespace BlazorTechNotes.Domain.Abstractions;

public class Result(bool isSuccessfull, string? ErrorMessage = null)
{
  public bool IsSuccessfull { get; } = isSuccessfull;
  public bool HasFailed => !IsSuccessfull;
  public string? ErrorMessage { get; } = ErrorMessage;

  public static Result Ok() => new(true);
  public static Result Fail(string errorMessage) => new(false, errorMessage);

  public static Result<T> Ok<T>(T? value) => new(value, true, string.Empty);
  public static Result<T> Fail<T>(string errorMessage) => new(default, false, errorMessage);

  public static Result<T> FromValue<T>(T? value, string? errorMessage = null)
  {
    if (value is null)
    {
      return Fail<T>(errorMessage ?? "Value cannot be null.");
    }

    return Ok(value);
  }
  
}

public class Result<T>(T? value, bool isSuccessfull, string? errorMessage = null) : Result(isSuccessfull, errorMessage)
{
  public T? Value { get; } = value;

  public static implicit operator Result<T>(T? value) => FromValue(value);

  public static implicit operator T?(Result<T> result) => result.Value;
}

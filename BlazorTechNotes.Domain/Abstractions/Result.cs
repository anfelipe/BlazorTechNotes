namespace BlazorTechNotes.Domain.Abstractions;

public class Result<T>
{
  public bool IsSuccess { get; }
  public T? Value { get; }
  public ResultError Error { get; }
  public bool IsFailure => !IsSuccess;

  internal Result(bool isSuccess, T? value, ResultError error)
  {

    if (isSuccess && error != ResultError.None)
      throw new InvalidOperationException();

    IsSuccess = isSuccess;
    Value = value;
    Error = error;
  }
}

public static class Result
{
  public static Result<T> Success<T>() => new(true, default!, ResultError.None);
  public static Result<T> Success<T>(T value) => new(true, value, ResultError.None);
  public static Result<T> Failure<T>(ResultError error) => new(false, default, error);
  public static Result<T> Failure<T>(ResultError error, T value) => new(false, value, error);
}

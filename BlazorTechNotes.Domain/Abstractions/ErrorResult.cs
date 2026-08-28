
using BlazorTechNotes.Domain.Enums;

namespace BlazorTechNotes.Domain.Abstractions;

public sealed record class ResultError(string Code, string Description, ErrorType Type)
{
  public static readonly ResultError None = new(string.Empty, string.Empty, ErrorType.None);
}
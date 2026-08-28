using BlazorTechNotes.Domain.Abstractions;
using BlazorTechNotes.Domain.Enums;

namespace BlazorTechNotes.Domain.Entities;

public static class SharedErrors
{
  public static readonly ResultError InvalidRequest = new(
    Code: "Shared.InvalidRequest",
    Description: "The request is invalid.",
    Type: ErrorType.Validation
  );
}

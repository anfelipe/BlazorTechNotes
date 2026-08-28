
using BlazorTechNotes.Domain.Abstractions;
using BlazorTechNotes.Domain.Enums;

namespace BlazorTechNotes.Domain.Entities.Notes;

public static class NotesErrors
{

  public static readonly ResultError NotFound = new(
    Code: "Notes.NotFound",
    Description: "The requested note was not found.",
    Type: ErrorType.NotFound
  );

}

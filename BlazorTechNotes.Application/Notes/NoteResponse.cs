namespace BlazorTechNotes.Application.Notes;

public record struct NoteResponse
(
  int Id,
  string Title,
  string? Content,
  DateTime PublishedAt,
  DateTime CreatedAt,
  bool IsPublished,
  string? UserName,
  string UserId,
  bool CanEdit
);

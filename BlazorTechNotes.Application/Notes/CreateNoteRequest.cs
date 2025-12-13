
namespace BlazorTechNotes.Application.Notes;

public record CreateNoteRequest
{
  public required string Title { get; set; }
  public string? Content { get; set; }
  public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
  public bool IsPublished { get; set; } = false;
  public string? UserId { get; set; } = null;
}

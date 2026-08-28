using BlazorTechNotes.Domain.Abstractions;

namespace BlazorTechNotes.Domain.Entities.Notes;

public class Note : Entity
{
  public required string Title { get; set; }
  public string? Content { get; set; }
  public DateTime? PublishedAt { get; set; }
  public bool IsPublished { get; set; }
  public string? UserId { get; set; }
}

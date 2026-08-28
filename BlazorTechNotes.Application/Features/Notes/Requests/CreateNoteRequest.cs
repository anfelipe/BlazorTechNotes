namespace BlazorTechNotes.Application.Features.Notes.Requests;

public record CreateNoteRequest
{
    public required string Title { get; set; }
    public string? Content { get; set; }
    public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
    public bool IsPublished { get; set; }
    public string? UserId { get; set; }
}

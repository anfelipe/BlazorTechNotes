using BlazorTechNotes.Domain.Entities.Notes;

namespace BlazorTechNotes.Application.Features.Notes.Abstractions;

public interface INoteRepository
{
    Task<List<Note>> GetAllNotesAsync();
    Task<Note?> GetNoteByIdAsync(int id);
    Task<Note> CreateNoteAsync(Note note);
    Task<Note?> UpdateNoteAsync(Note note);
    Task<bool> DeleteNoteAsync(int id);
}

using BlazorTechNotes.Application.Features.Notes.Requests;
using BlazorTechNotes.Application.Features.Notes.Responses;

namespace BlazorTechNotes.Application.Features.Notes.Abstractions;

public interface INoteService
{
    Task<Result<List<NoteResponse>>> GetAllNotesAsync();
    Task<Result<NoteResponse>> GetNoteByIdAsync(int id);
    Task<Result<NoteResponse>> CreateNoteAsync(CreateNoteRequest note);
    Task<Result<NoteResponse>> UpdateNoteAsync(UpdateNoteRequest note);
    Task<Result<bool>> DeleteNoteAsync(int id);
}

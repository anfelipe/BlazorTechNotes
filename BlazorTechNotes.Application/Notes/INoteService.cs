namespace BlazorTechNotes.Application.Notes;

public interface INoteService
{
  Task<Result<List<NoteResponse>>> GetAllNotesAsync();
  Task<Result<NoteResponse?>> GetNoteByIdAsync(int id);
  Task<Result<NoteResponse>> CreateNoteAsync(CreateNoteRequest note);
  Task<Result<NoteResponse?>> UpdateNoteAsync(UpdateNoteRequest note);
  Task<Result> DeleteNoteAsync(int id);
}

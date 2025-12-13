using BlazorTechNotes.Domain.User;

namespace BlazorTechNotes.Application.Notes;

public class NoteService(INoteRepository noteRepository, IUserRepository userRepository) : INoteService
{
  private readonly INoteRepository _noteRepository = noteRepository;
  private readonly IUserRepository _userRepository = userRepository;

  public async Task<Result<NoteResponse>> CreateNoteAsync(CreateNoteRequest request)
  {
    Note newNote = request.Adapt<Note>();
    var note = await _noteRepository.CreateNoteAsync(newNote);
    return note.Adapt<NoteResponse>();
  }

  public async Task<Result> DeleteNoteAsync(int id)
  {
    var deleted = await _noteRepository.DeleteNoteAsync(id);

    if (deleted)
    {
      return Result.Ok();
    }

    return Result.Fail("Note not found.");
  }

  public async Task<Result<List<NoteResponse>>> GetAllNotesAsync()
  {
    var notes = await _noteRepository.GetAllNotesAsync();
    var response = new List<NoteResponse>();

    foreach (var note in notes)
    {
      var noteResponse = note.Adapt<NoteResponse>();

      if (note.UserId is not null)
      {
        var author = await _userRepository.GetUserByIdAsync(note.UserId);
        noteResponse.UserName = author?.UserName ?? "Unknown";
      }
      else
      {
        noteResponse.UserName = "Unknown";
      }

      response.Add(noteResponse);
    }

    return response
    .OrderByDescending(n => n.PublishedAt)
    .ToList();
  }

  public async Task<Result<NoteResponse?>> GetNoteByIdAsync(int id)
  {
    var note = await _noteRepository.GetNoteByIdAsync(id);

    if (note is null)
    {
      return Result.Fail<NoteResponse?>("Note not found.");
    }

    var noteResponse = note.Adapt<NoteResponse>();

    if (note.UserId is not null)
    {
      var user = await _userRepository.GetUserByIdAsync(note.UserId);
      noteResponse.UserName = user?.UserName ?? "Unknown";
    }
    else
    {
      noteResponse.UserName = "Unknown";
    }

    return noteResponse;
  }

  public async Task<Result<NoteResponse?>> UpdateNoteAsync(UpdateNoteRequest request)
  {
    Note noteToUpdate = request.Adapt<Note>();
    var updatedNote = await _noteRepository.UpdateNoteAsync(noteToUpdate);

    if (updatedNote is null)
    {
      return Result.Fail<NoteResponse?>("Note not found.");
    }

    return updatedNote?.Adapt<NoteResponse>();
  }
}

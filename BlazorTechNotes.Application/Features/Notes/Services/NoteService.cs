using BlazorTechNotes.Application.Exceptions;
using BlazorTechNotes.Application.Features.Notes.Abstractions;
using BlazorTechNotes.Application.Features.Notes.Requests;
using BlazorTechNotes.Application.Features.Notes.Responses;
using BlazorTechNotes.Application.Features.Users.Abstractions;

namespace BlazorTechNotes.Application.Features.Notes.Services;

public class NoteService(
  INoteRepository noteRepository,
  IUserRepository userRepository) : INoteService
{
  private readonly INoteRepository _noteRepository = noteRepository;
  private readonly IUserRepository _userRepository = userRepository;

  public async Task<Result<NoteResponse>> CreateNoteAsync(CreateNoteRequest note)
  {
    try
    {
      Note newNote = note.Adapt<Note>();
      var userId = await _userRepository.GetCurrentUserIdAsync();

      if (userId is null)
        return Result.Failure<NoteResponse>(UserErrors.Unauthorized);

      var isAuthorized = await _userRepository.CurrentUserCanCreateNoteAsync();
      if (!isAuthorized)
        return Result.Failure<NoteResponse>(UserErrors.Unauthorized);

      newNote.UserId = userId;

      var noteCreated = await _noteRepository.CreateNoteAsync(newNote);
      return Result.Success(noteCreated.Adapt<NoteResponse>());
    }
    catch (UserNotAuthorizedException)
    {
      return Result.Failure<NoteResponse>(UserErrors.Unauthorized);
    }
  }

  public async Task<Result<bool>> DeleteNoteAsync(int id)
  {
    var userCanDelete = await _userRepository.CurrentUserCanEditNoteAsync(id);

    if (!userCanDelete)
    {
      return Result.Failure<bool>(UserErrors.Unauthorized);
    }

    var deleted = await _noteRepository.DeleteNoteAsync(id);

    return deleted 
      ? Result.Success(true) 
      : Result.Failure<bool>(NotesErrors.NotFound);
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
        noteResponse.UserId = note.UserId;
        noteResponse.CanEdit = await _userRepository.CurrentUserCanEditNoteAsync(note.Id);
      }
      else
      {
        noteResponse.UserName = "Unknown";
      }

      response.Add(noteResponse);
    }

    return Result.Success(response
    .OrderByDescending(n => n.PublishedAt)
    .ToList()
    );
  }

  public async Task<Result<NoteResponse>> GetNoteByIdAsync(int id)
  {
    var note = await _noteRepository.GetNoteByIdAsync(id);

    if (note is null)
    {
      return Result.Failure<NoteResponse>(NotesErrors.NotFound);
    }

    var noteResponse = note.Adapt<NoteResponse>();

    if (note.UserId is not null)
    {
      var user = await _userRepository.GetUserByIdAsync(note.UserId);
      noteResponse.UserName = user?.UserName ?? "Unknown";
      noteResponse.UserId = note.UserId;
      noteResponse.CanEdit = await _userRepository.CurrentUserCanEditNoteAsync(note.Id);
    }
    else
    {
      noteResponse.UserName = "Unknown";
    }

    return Result.Success(noteResponse);
  }

  public async Task<Result<NoteResponse>> UpdateNoteAsync(UpdateNoteRequest note)
  {
    if (note is null)
    {
      return Result.Failure<NoteResponse>(SharedErrors.InvalidRequest);
    }

    Note noteToUpdate = note.Adapt<Note>();
    var userCanEdit = await _userRepository.CurrentUserCanEditNoteAsync(noteToUpdate.Id);

    if (!userCanEdit)
    {
      return Result.Failure<NoteResponse>(UserErrors.Unauthorized);
    }

    var updatedNote = await _noteRepository.UpdateNoteAsync(noteToUpdate);

    return updatedNote is null 
      ? Result.Failure<NoteResponse>(NotesErrors.NotFound) 
      : Result.Success(updatedNote.Adapt<NoteResponse>());
  }
}

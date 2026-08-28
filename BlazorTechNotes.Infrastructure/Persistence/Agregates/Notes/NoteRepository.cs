using BlazorTechNotes.Application.Features.Notes.Abstractions;
using BlazorTechNotes.Domain.Entities.Notes;
using BlazorTechNotes.Infrastructure.Persistence.Contexts;

namespace BlazorTechNotes.Infrastructure.Persistence.Agregates.Notes;

public class NoteRepository(ApplicationDbContext context) : INoteRepository
{

  private readonly ApplicationDbContext _context = context;

  public async Task<Note> CreateNoteAsync(Note note)
  {
    _context.Notes.Add(note);
    await _context.SaveChangesAsync();

    return note;
  }

  public async Task<bool> DeleteNoteAsync(int id)
  {
    var noteToDelete = await GetNoteByIdAsync(id);

    if(noteToDelete is null) return false;

    _context.Notes.Remove(noteToDelete);
    await _context.SaveChangesAsync();
    
    return true;
  }

  public async Task<List<Note>> GetAllNotesAsync() => await _context.Notes.ToListAsync();

  public async Task<Note?> GetNoteByIdAsync(int id) => await _context.Notes.FindAsync(id);

  public async Task<Note?> UpdateNoteAsync(Note note)
  {

    if(note is null) return null;

    var noteToUpdate = await GetNoteByIdAsync(note.Id);

    if(noteToUpdate is null) return null;

    noteToUpdate.Title = note.Title;
    noteToUpdate.Content = note.Content;
    noteToUpdate.IsPublished = note.IsPublished;
    noteToUpdate.PublishedAt = note.PublishedAt;
    noteToUpdate.UpdatedAt = DateTime.Now;

    _context.Notes.Update(noteToUpdate);
    await _context.SaveChangesAsync();
    
    return noteToUpdate;
  }
}

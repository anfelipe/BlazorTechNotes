using System.Collections.ObjectModel;

using BlazorTechNotes.Domain.Entities.Notes;

namespace BlazorTechNotes.Domain.Entities.Users;

public interface IUser
{
  public string Id { get; set; }
  public string? UserName { get; set; }
  public string? Email { get; set; }
  public Collection<Note> Notes { get; set; }
}

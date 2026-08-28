using System.Collections.ObjectModel;

using BlazorTechNotes.Domain.Entities.Notes;
using BlazorTechNotes.Domain.Entities.Users;

namespace BlazorTechNotes.Infrastructure.Common;

public class User : IdentityUser, IUser
{
  public Collection<Note> Notes { get; set; } = [];
}

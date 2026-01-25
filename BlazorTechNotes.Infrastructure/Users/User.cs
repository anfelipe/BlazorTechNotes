using BlazorTechNotes.Domain.Notes;
using BlazorTechNotes.Domain.User;
using Microsoft.AspNetCore.Identity;

namespace BlazorTechNotes.Infrastructure.Users;

public class User : IdentityUser, IUser
{
  public List<Note> Notes { get; set; } = [];
}

namespace BlazorTechNotes.Application.Users;

public interface IUserService
{
  Task<string> GetCurrentUserIdAsync();
  Task<bool> IsCurrentUserInRoleAsync(string roleName);
  Task<bool> CurrentUserCanCreateNoteAsync();
  Task<bool> CurrentUserCanEditNoteAsync(int noteId);
}

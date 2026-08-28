using BlazorTechNotes.Domain.Entities.Users;

namespace BlazorTechNotes.Application.Features.Users.Abstractions;

public interface IUserRepository
{
  Task<List<IUser>> GetAllUsersAsync();
  Task<IUser?> GetUserByIdAsync(string userId);
  Task<string> GetCurrentUserIdAsync();
  Task<bool> IsCurrentUserInRoleAsync(string roleName);
  Task<bool> CurrentUserCanCreateNoteAsync();
  Task<bool> CurrentUserCanEditNoteAsync(int noteId);
  Task<List<string>> GetCurrentUserRolesAsync(string userId);
  Task<bool> AddUserToRoleAsync(string userId, string roleName);
  Task<bool> RemoveUserFromRoleAsync(string userId, string roleName);
}

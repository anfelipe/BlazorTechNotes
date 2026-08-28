namespace BlazorTechNotes.Application.Features.Users.Abstractions;

public interface IUserRolesService
{
  Task<Result<List<string>>> GetUserRolesAsync(string userId);
  Task<Result<bool>> AddUserRoleAsync(string userId, string role);
  Task<Result<bool>> RemoveUserRoleAsync(string userId, string role);
}

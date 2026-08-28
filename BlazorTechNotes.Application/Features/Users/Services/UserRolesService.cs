using BlazorTechNotes.Application.Features.Users.Abstractions;

namespace BlazorTechNotes.Application.Features.Users.Services;

public class UserRolesService(IUserRepository userRepository) : IUserRolesService
{
  private readonly IUserRepository _userRepository = userRepository;

  public async Task<Result<List<string>>> GetUserRolesAsync(string userId)
  {
    var roles = await _userRepository.GetCurrentUserRolesAsync(userId);
    return Result.Success(roles);
  }

  public async Task<Result<bool>> AddUserRoleAsync(string userId, string role)
  {
    var success = await _userRepository.AddUserToRoleAsync(userId, role);
    return success
      ? Result.Success(true)
      : Result.Failure<bool>(UserErrors.AddUserRoleFailed);
  }

  public async Task<Result<bool>> RemoveUserRoleAsync(string userId, string role)
  {
    var success = await _userRepository.RemoveUserFromRoleAsync(userId, role);
    return success
      ? Result.Success(true)
      : Result.Failure<bool>(UserErrors.RemoveUserRoleFailed);
  }

}

using BlazorTechNotes.Domain.User;
using BlazorTechNotes.Infrastructure.Users;
using Microsoft.AspNetCore.Identity;

namespace BlazorTechNotes.Infrastructure.Repositories;

public class UserRepository(UserManager<User> userManager) : IUserRepository
{
  private readonly UserManager<User> _userManager = userManager;

  public async Task<IUser?> GetUserByIdAsync(string userId)
  {
    return await _userManager.FindByIdAsync(userId);
  }
}

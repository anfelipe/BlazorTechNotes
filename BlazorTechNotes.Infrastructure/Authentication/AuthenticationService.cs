using BlazorTechNotes.Application.Authentication;
using Microsoft.AspNetCore.Identity;

namespace BlazorTechNotes.Infrastructure.Authentication;

public class AuthenticationService(SignInManager<User> signInManager, UserManager<User> userManager) : IAuthenticationService
{
  private readonly SignInManager<User> _signInManager = signInManager;
  private readonly UserManager<User> _userManager = userManager;

  public async Task<bool> LoginUserAsync(string userName, string password)
  {
    var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent: false, lockoutOnFailure: false);
    return result.Succeeded;
  }

  public async Task<RegisterUserResponse> RegisterUserAsync(string UserName, string email, string password)
  {
    var response = new RegisterUserResponse();

    var user = new User
    {
      UserName = UserName,
      Email = email,
      EmailConfirmed = true
    };

    var result = await _userManager.CreateAsync(user, password);

    if (result.Succeeded)
    {
      await _userManager.AddToRoleAsync(user, "Reader");
    }

    return new RegisterUserResponse
    {
      Succeeded = result.Succeeded,
      Errors = [.. result.Errors.Select(e => e.Description)]
    };

  }
}

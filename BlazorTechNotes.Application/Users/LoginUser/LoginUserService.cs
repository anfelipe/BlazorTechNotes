
using BlazorTechNotes.Application.Authentication;

namespace BlazorTechNotes.Application.Users.LoginUser;

public class LoginUserService(IAuthenticationService authenticationService) : ILoginUserService
{
  private readonly IAuthenticationService _authenticationService = authenticationService;

  public async Task<Result> LoginUserAsync(LoginUserRequest request)
  {
    var result = await _authenticationService.LoginUserAsync(request.UserName, request.Password);

    if(!result) return Result.Fail("Invalid username or password.");
    
    return Result.Ok();
  }
}

using BlazorTechNotes.Application.Authentication;
using BlazorTechNotes.Application.Features.Users.Abstractions;
using BlazorTechNotes.Application.Features.Users.Requests;

namespace BlazorTechNotes.Application.Features.Users.Services;

public class LoginUserService(IAuthenticationService authenticationService) : ILoginUserService
{
  private readonly IAuthenticationService _authenticationService = authenticationService;

  public async Task<Result<bool>> LoginUserAsync(LoginUserRequest request)
  {
    if (request is null)
    {
      return Result.Failure<bool>(SharedErrors.InvalidRequest);
    }
    
    var result = await _authenticationService.LoginUserAsync(request.UserName, request.Password);

    return result
      ? Result.Success(true)
      : Result.Failure<bool>(UserErrors.LoginFailed)
      ;
  }
}

using System;
using BlazorTechNotes.Application.Authentication;

namespace BlazorTechNotes.Application.Users.RegisterUser;

public class RegisterUserService(IAuthenticationService authenticationService) : IRegisterUserService
{
  private readonly IAuthenticationService _authenticationService = authenticationService;

  public async Task<Result<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest request)
  {
    var response = await _authenticationService.RegisterUserAsync(request.UserName, request.Email, request.Password);

    if (!response.Succeeded)
      return Result.Fail<RegisterUserResponse>(string.Join(", ", response.Errors));

    return Result.Ok(response);
  }
}

using BlazorTechNotes.Application.Authentication;
using BlazorTechNotes.Application.Features.Users.Abstractions;
using BlazorTechNotes.Application.Features.Users.Requests;

namespace BlazorTechNotes.Application.Features.Users.Services;

public class RegisterUserService(IAuthenticationService authenticationService) : IRegisterUserService
{
  private readonly IAuthenticationService _authenticationService = authenticationService;

  public async Task<Result<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest request)
  {
    if (request is null)
    {
      return Result.Failure<RegisterUserResponse>(SharedErrors.InvalidRequest);
    }

    var response = await _authenticationService.RegisterUserAsync(request.UserName, request.Email, request.Password);

    return response.Succeeded
      ? Result.Success(response)
      : Result.Failure<RegisterUserResponse>(UserErrors.RegisterUserFailed);
  }
}

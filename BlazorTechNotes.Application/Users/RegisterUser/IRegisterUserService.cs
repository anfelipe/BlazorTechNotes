using BlazorTechNotes.Application.Authentication;

namespace BlazorTechNotes.Application.Users.RegisterUser;

public interface IRegisterUserService
{
  Task<Result<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest request);
}

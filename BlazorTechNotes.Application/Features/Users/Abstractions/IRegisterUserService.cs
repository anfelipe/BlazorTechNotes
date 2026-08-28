using BlazorTechNotes.Application.Authentication;
using BlazorTechNotes.Application.Features.Users.Requests;

namespace BlazorTechNotes.Application.Features.Users.Abstractions;

public interface IRegisterUserService
{
  Task<Result<RegisterUserResponse>> RegisterUserAsync(RegisterUserRequest request);
}

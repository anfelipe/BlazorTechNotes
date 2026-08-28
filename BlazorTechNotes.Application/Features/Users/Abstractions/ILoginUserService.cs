using BlazorTechNotes.Application.Features.Users.Requests;

namespace BlazorTechNotes.Application.Features.Users.Abstractions;

public interface ILoginUserService
{
  Task<Result<bool>> LoginUserAsync(LoginUserRequest request);
}

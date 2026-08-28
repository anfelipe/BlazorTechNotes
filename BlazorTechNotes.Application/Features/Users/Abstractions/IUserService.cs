using BlazorTechNotes.Application.Features.Users.Responses;

namespace BlazorTechNotes.Application.Features.Users.Abstractions;

public interface IUserService
{
  Task<Result<List<UserResponse>>> GetAllUsersAsync();
}

using BlazorTechNotes.Application.Features.Users.Abstractions;
using BlazorTechNotes.Application.Features.Users.Responses;

namespace BlazorTechNotes.Application.Features.Users.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
	private readonly IUserRepository _userRepository = userRepository;

	public async Task<Result<List<UserResponse>>> GetAllUsersAsync()
	{

		if (!await _userRepository.IsCurrentUserInRoleAsync("Admin"))
		{
			return Result.Failure<List<UserResponse>>(UserErrors.Unauthorized);
		}

		var users = await _userRepository.GetAllUsersAsync();

		var response = new List<UserResponse>();

		foreach (var user in users)
		{
			var roles = await _userRepository.GetCurrentUserRolesAsync(user.Id);

			var userResponse = user.Adapt<UserResponse>();
			userResponse.Roles = string.Join(", ", roles);

			response.Add(userResponse);
		}

		return Result.Success(response);
	}
}

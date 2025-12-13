namespace BlazorTechNotes.Application.Users.LoginUser;

public interface ILoginUserService
{
  Task<Result> LoginUserAsync(LoginUserRequest request);
}

namespace BlazorTechNotes.Application.Authentication;

public interface IAuthenticationService
{
  Task<RegisterUserResponse> RegisterUserAsync(string UserName, string email, string password);
  Task<bool> LoginUserAsync(string userName, string password);
}

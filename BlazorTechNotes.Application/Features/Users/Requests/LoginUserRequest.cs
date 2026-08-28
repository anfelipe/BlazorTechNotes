namespace BlazorTechNotes.Application.Features.Users.Requests;

public class LoginUserRequest
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}

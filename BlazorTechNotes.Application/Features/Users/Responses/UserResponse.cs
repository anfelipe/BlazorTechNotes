namespace BlazorTechNotes.Application.Features.Users.Responses;

public record struct UserResponse(string Id, string UserName, string Email, string Roles)
{

}

namespace BlazorTechNotes.Domain.User;

public interface IUserRepository
{
  Task<IUser?> GetUserByIdAsync(string userId);
}

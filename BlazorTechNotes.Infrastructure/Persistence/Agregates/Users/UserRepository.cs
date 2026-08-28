using BlazorTechNotes.Application.Exceptions;
using BlazorTechNotes.Application.Features.Notes.Abstractions;
using BlazorTechNotes.Application.Features.Users.Abstractions;
using BlazorTechNotes.Domain.Entities.Users;
using BlazorTechNotes.Infrastructure.Common;
using Microsoft.AspNetCore.Http;

namespace BlazorTechNotes.Infrastructure.Persistence.Agregates.Users;

public class UserRepository(
  UserManager<User> userManager,
  RoleManager<IdentityRole> roleManager,
  IHttpContextAccessor httpContextAccessor,
  INoteRepository noteRepository
  ) : IUserRepository
{

  private readonly UserManager<User> _userManager = userManager;
  private readonly RoleManager<IdentityRole> _roleManager = roleManager;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
  private readonly INoteRepository _noteRepository = noteRepository;


  public async Task<List<IUser>> GetAllUsersAsync()
  {
    return await _userManager.Users
    .AsNoTracking()
    .Select(u => (IUser)u)
    .ToListAsync()
    ;
  }

  public async Task<IUser?> GetUserByIdAsync(string userId)
  {
    return await _userManager.FindByIdAsync(userId);
  }

  public async Task<string> GetCurrentUserIdAsync()
  {
    var user = await GetCurrentUserAsync() ?? throw new UserNotAuthorizedException();
    return user.Id;
  }

  public async Task<bool> CurrentUserCanCreateNoteAsync()
  {
    var user = await GetCurrentUserAsync();

    if (user is null) return false;

    var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
    var isWriter = await _userManager.IsInRoleAsync(user, "Writer");

    return isAdmin || isWriter;
  }

  public async Task<bool> CurrentUserCanEditNoteAsync(int noteId)
  {
    var user = await GetCurrentUserAsync();

    if (user is null) return false;

    var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
    var isWriter = await _userManager.IsInRoleAsync(user, "Writer");
    var note = await _noteRepository.GetNoteByIdAsync(noteId);

    if (note is null) return false;

    return isAdmin || (isWriter && note.UserId == user.Id);
  }

  public async Task<bool> IsCurrentUserInRoleAsync(string roleName)
  {
    var user = await GetCurrentUserAsync();
    var isInRole = user is not null && await _userManager.IsInRoleAsync(user, roleName);

    return isInRole;
  }

  private async Task<User?> GetCurrentUserAsync()
  {
    var httpContext = _httpContextAccessor.HttpContext;

    if (httpContext is { User: null }) return null;

    var user = await _userManager.GetUserAsync(httpContext!.User);

    return user;
  }

  public async Task<List<string>> GetCurrentUserRolesAsync(string userId)
  {
    var user = await _userManager.FindByIdAsync(userId);

    if (user is null) return [];

    var roles = await _userManager.GetRolesAsync(user);

    return [.. roles];
  }

  public async Task<bool> AddUserToRoleAsync(string userId, string roleName)
  {
    var user = await _userManager.FindByIdAsync(userId);

    if (user is null) return false;

    if (!await _roleManager.RoleExistsAsync(roleName))
    {
      var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

      if (!roleResult.Succeeded) return false;
    }

    var result = await _userManager.AddToRoleAsync(user, roleName);

    if (!result.Succeeded) return false;

    return true;
  }

  public async Task<bool> RemoveUserFromRoleAsync(string userId, string roleName)
  {
    var user = await _userManager.FindByIdAsync(userId);

    if (user is null) return false;

    var result = await _userManager.RemoveFromRoleAsync(user, roleName);

    if (!result.Succeeded) return false;

    return true;
  }
}

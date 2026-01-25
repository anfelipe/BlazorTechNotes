using System.Reflection.Metadata.Ecma335;
using BlazorTechNotes.Application.Exceptions;
using BlazorTechNotes.Application.Users;
using BlazorTechNotes.Domain.Notes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BlazorTechNotes.Infrastructure.Users;

public class UserService(
  UserManager<User> userManager,
  IHttpContextAccessor httpContextAccessor,
  INoteRepository noteRepository
  ) : IUserService
{

  private readonly UserManager<User> _userManager = userManager;
  private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
  private readonly INoteRepository _noteRepository = noteRepository;


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
}

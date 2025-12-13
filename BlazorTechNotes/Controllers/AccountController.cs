using System.Security.Claims;
using BlazorTechNotes.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorTechNotes.Controllers;

[Route("account")]
public class AccountController(SignInManager<User> signInManager, UserManager<User> userManager) : Controller
{
  private readonly SignInManager<User> _signInManager = signInManager;
  private readonly UserManager<User> _userManager = userManager;

  [AllowAnonymous]
  [HttpPost("external-login")]
  public async Task<IActionResult> ExternalLogin(string provider)
  {
    var redirectUrl = Url.Action(nameof(HandleExternalLoginCallback));
    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
    return Challenge(properties, provider);
  }

  [AllowAnonymous]
  [HttpGet("external-callback")]
  public async Task<IActionResult> HandleExternalLoginCallback()
  {
    var info = await _signInManager.GetExternalLoginInfoAsync();

    if (info is null)
    {
      return RedirectWithError("Error loading external login information.");
    }

    var signInResult = await _signInManager.ExternalLoginSignInAsync(
      info.LoginProvider, info.ProviderKey, isPersistent: false);

    if (signInResult.Succeeded)
    {
      return Redirect("/notes");
    }

    var email = info.Principal.FindFirstValue(ClaimTypes.Email);

    if (string.IsNullOrEmpty(email))
    {
      return RedirectWithError("Email claim not received from external provider.");
    }

    var user = await _userManager.FindByEmailAsync(email) ?? new User
    { UserName = email, Email = email, EmailConfirmed = true };

    await _userManager.CreateAsync(user);
    await _userManager.AddLoginAsync(user, info);
    await _signInManager.SignInAsync(user, isPersistent: false);

    return Redirect("/notes");
  }

  [Authorize]
  [HttpPost("logout")]
  public async Task<IActionResult> Logout()
  {
    await _signInManager.SignOutAsync();
    return Redirect("/notes");
  }

  private IActionResult RedirectWithError(string errorMessage)
  {
    var encodedMessage = Uri.EscapeDataString(errorMessage);
    return Redirect($"/register?error={encodedMessage}");
  }

}

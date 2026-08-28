using BlazorTechNotes.Application.Features.Users.Abstractions;
using BlazorTechNotes.Application.Features.Users.Requests;
using Microsoft.AspNetCore.Components;

namespace BlazorTechNotes.Features.Users.Components;

public partial class LoginUser
{
  [Inject]
  internal NavigationManager NavigationManager { get; set; } = default!;

  [Inject]
  internal ILoginUserService LoginUserService { get; set; } = default!;

  [SupplyParameterFromForm]
  public required LoginUserModel LoginUserModel { get; set; }

  private string errorMessage = string.Empty;

  protected override void OnInitialized()
  {
    LoginUserModel ??= new();

    var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
    if (Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query).TryGetValue("error", out var error))
    {
      errorMessage = error.ToString();
    }
  }

  private async Task HandleSubmit()
  {
    var command = new LoginUserRequest
    {
      UserName = LoginUserModel.UserName,
      Password = LoginUserModel.Password
    };

    var result = await LoginUserService.LoginUserAsync(command);

    if (!result.IsSuccess)
    {
      errorMessage = result.Error.Description ?? "Ha ocurrido un error al iniciar sesion.";
      return;
    }

    NavigationManager.NavigateTo("/notes");
  }
}

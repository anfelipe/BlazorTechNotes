using BlazorTechNotes.Application.Features.Users.Abstractions;
using BlazorTechNotes.Application.Features.Users.Requests;
using Microsoft.AspNetCore.Components;

namespace BlazorTechNotes.Features.Users.Components;

public partial class RegisterUser
{
  [Inject]
  public NavigationManager NavigationManager { get; set; } = default!;

  [Inject]
  public IRegisterUserService RegisterUserService { get; set; } = default!;

  [SupplyParameterFromForm]
  public required RegisterUserModel UserModel { get; set; }

  private string _errorMessage = string.Empty;

  protected override void OnInitialized()
  {
    UserModel ??= new();
    base.OnInitialized();
  }

  internal async Task HandleSubmit()
  {
    var command = new RegisterUserRequest
    {
      UserName = UserModel.UserName,
      Email = UserModel.Email,
      Password = UserModel.Password
    };

    var result = await RegisterUserService.RegisterUserAsync(command);

    if (result.IsFailure)
    {
      _errorMessage = result.Error.Description ?? "Ha ocurrido un error en el registro.";
      return;
    }

    NavigationManager.NavigateTo("/login");
  }
}

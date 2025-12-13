using BlazorTechNotes.Application.Users.RegisterUser;
using Microsoft.AspNetCore.Components;

namespace BlazorTechNotes.Features.Users.Components;

public partial class RegisterUser
{
  [Inject]
  internal NavigationManager NavigationManager { get; set; } = default!;

  [Inject]
  internal IRegisterUserService RegisterUserService { get; set; } = default!;

  [SupplyParameterFromForm]
  public required RegisterUserModel UserModel { get; set; }

  private string errorMessage = string.Empty;

  protected override void OnInitialized()
  {
    UserModel ??= new();
    base.OnInitialized();
  }

  async Task HandleSubmit()
  {
    var command = new RegisterUserRequest
    {
      UserName = UserModel.UserName,
      Email = UserModel.Email,
      Password = UserModel.Password
    };

    var result = await RegisterUserService.RegisterUserAsync(command);

    if (!result.IsSuccessfull)
    {
      errorMessage = result.ErrorMessage ?? "Ha ocurrido un error en el registro.";
      return;
    }

    NavigationManager.NavigateTo("/login");
  }
}

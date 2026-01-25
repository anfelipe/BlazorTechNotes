using BlazorTechNotes.Infrastructure.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace BlazorTechNotes.Features.Users.Components;

public partial class LogoutUser : ComponentBase
{

  [Inject]
  private SignInManager<User> SignInManager { get; set; } = null!;

  [Inject]
  private NavigationManager NavigationManager { get; set; } = null!;

  protected override async Task OnInitializedAsync()
  {
    await SignInManager.SignOutAsync();
    StateHasChanged();
    NavigationManager.NavigateTo("/notes");
  }
}

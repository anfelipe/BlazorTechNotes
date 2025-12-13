using BlazorTechNotes.Application.Notes;
using BlazorTechNotes.Application.Users.LoginUser;
using BlazorTechNotes.Application.Users.RegisterUser;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTechNotes.Application;

public static class DependencyInjection
{

  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<INoteService, NoteService>();
    services.AddScoped<ILoginUserService, LoginUserService>();
    services.AddScoped<IRegisterUserService, RegisterUserService>();
    return services;
  }

}

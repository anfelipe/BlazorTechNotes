using BlazorTechNotes.Application.Features.Notes.Abstractions;
using BlazorTechNotes.Application.Features.Notes.Services;
using BlazorTechNotes.Application.Features.Users.Abstractions;
using BlazorTechNotes.Application.Features.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorTechNotes.Application;

public static class DependencyInjectionApplication
{

  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<INoteService, NoteService>();
    services.AddScoped<ILoginUserService, LoginUserService>();
    services.AddScoped<IRegisterUserService, RegisterUserService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IUserRolesService, UserRolesService>();

    return services;
  }

}

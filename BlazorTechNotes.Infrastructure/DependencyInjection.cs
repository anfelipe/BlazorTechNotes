
using BlazorTechNotes.Application.Authentication;
using BlazorTechNotes.Infrastructure.Middleware;
using BlazorTechNotes.Domain.Notes;
using BlazorTechNotes.Domain.User;
using BlazorTechNotes.Infrastructure.Authentication;
using BlazorTechNotes.Infrastructure.Repositories;
using BlazorTechNotes.Infrastructure.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlazorTechNotes.Application.Users;

namespace BlazorTechNotes.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<ApplicationDbContext>(options =>
      options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
      );

    services.AddScoped<INoteRepository, NoteRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IUserService, UserService>();

    AddAuthentication(services);

    services.AddHttpContextAccessor();

    return services;
  }

  public static void AddAuthentication(IServiceCollection services)
  {
    services.AddSingleton<IAuthorizationMiddlewareResultHandler, BlazorAuthorizationMiddlewareResutlHandler>();
    services.AddScoped<IAuthenticationService, AuthenticationService>();
    services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
    services.AddCascadingAuthenticationState();
    services.AddAuthorization();
    services.AddAuthentication(Options =>
    {
      Options.DefaultScheme = IdentityConstants.ApplicationScheme;
      Options.DefaultChallengeScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

    services.AddIdentityCore<User>()
      .AddRoles<IdentityRole>()
      .AddEntityFrameworkStores<ApplicationDbContext>()
      .AddSignInManager()
      .AddDefaultTokenProviders();
  }
}

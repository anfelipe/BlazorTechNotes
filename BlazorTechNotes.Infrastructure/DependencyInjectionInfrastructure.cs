
using BlazorTechNotes.Application.Authentication;
using BlazorTechNotes.Infrastructure.Middleware;
using BlazorTechNotes.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlazorTechNotes.Infrastructure.Common;
using BlazorTechNotes.Infrastructure.Persistence.Agregates.Notes;
using BlazorTechNotes.Infrastructure.Persistence.Agregates.Users;
using BlazorTechNotes.Infrastructure.Persistence.Contexts;
using BlazorTechNotes.Application.Features.Notes.Abstractions;
using BlazorTechNotes.Application.Features.Users.Abstractions;


namespace BlazorTechNotes.Infrastructure;

public static class DependencyInjectionInfrastructure
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

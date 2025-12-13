using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace BlazorTechNotes.Application.Middleware;

public class BlazorAuthorizationMiddlewareResutlHandler : IAuthorizationMiddlewareResultHandler
{
  public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
  {
    return next(context);
  }
}

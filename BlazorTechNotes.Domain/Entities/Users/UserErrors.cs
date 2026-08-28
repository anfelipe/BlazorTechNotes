
using BlazorTechNotes.Domain.Abstractions;
using BlazorTechNotes.Domain.Enums;

namespace BlazorTechNotes.Domain.Entities.Users;

public static class UserErrors
{
  public static readonly ResultError Unauthorized = new(
    Code: "User.Unauthorized",
    Description: "User is not authorized to perform this action.",
    Type: ErrorType.Unauthorized
  );

  public static readonly ResultError LoginFailed = new(
    Code: "User.LoginFailed",
    Description: "Invalid username or password.",
    Type: ErrorType.Validation
  );

  public static readonly ResultError AddUserRoleFailed = new(
    Code: "User.AddUserRoleFailed",
    Description: "Add user role failed.",
    Type: ErrorType.Conflict
  );

  public static readonly ResultError RemoveUserRoleFailed = new(
    Code: "User.RemoveUserRoleFailed",
    Description: "Remove user role failed.",
    Type: ErrorType.Conflict
  );

  public static readonly ResultError RegisterUserFailed = new(
    Code: "User.RegisterUserFailed",
    Description: "Register user failed.",
    Type: ErrorType.Conflict
  );
}

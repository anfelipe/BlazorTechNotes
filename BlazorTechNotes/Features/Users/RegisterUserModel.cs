using System.ComponentModel.DataAnnotations;

namespace BlazorTechNotes.Features.Users;

public class RegisterUserModel
{
  [Required(ErrorMessage = "The User name is required.")]
  public string UserName { get; set; } = string.Empty;

  [Required(ErrorMessage = "The Email is required.")]
  [EmailAddress(ErrorMessage = "The Email is not valid.")]
  public string Email { get; set; } = string.Empty;

  [Required(ErrorMessage = "The Password is required.")]
  [DataType(DataType.Password)]
  public string Password { get; set; } = string.Empty;

  [Required(ErrorMessage = "The Confirm Password is required.")]
  [DataType(DataType.Password)]
  [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
  public string ConfirmPassword { get; set; } = string.Empty;

}

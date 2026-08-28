using System.Collections.ObjectModel;

namespace BlazorTechNotes.Application.Authentication;

public class RegisterUserResponse
{
  public bool Succeeded { get; set; }
  public ReadOnlyCollection<string> Errors { get; set; } = [];
}

using BlazorTechNotes.Application.Features.Notes.Abstractions;
using BlazorTechNotes.Application.Features.Notes.Responses;
using Microsoft.AspNetCore.Components;

namespace BlazorTechNotes.Features.Notes.Components;

public partial class NoteView
{
  [Inject]
  private INoteService NoteService { get; set; } = default!;

  [Parameter]
  public int NoteId { get; set; }
  private NoteResponse? _note;
  private string _errorMessage = string.Empty;

  protected override async Task OnParametersSetAsync()
  {
    var result = await NoteService.GetNoteByIdAsync(NoteId);

    if (result is { IsSuccess: true})
    {
      _note = (NoteResponse)result.Value;
    }
    else
    {
      _errorMessage = result.Error.Description ?? "Lo sentimos, algo salió mal.";
    }
  }
}

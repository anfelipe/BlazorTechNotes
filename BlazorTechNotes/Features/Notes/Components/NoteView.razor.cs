using BlazorTechNotes.Application.Notes;
using Microsoft.AspNetCore.Components;

namespace BlazorTechNotes.Features.Notes.Components;

public partial class NoteView
{
  [Inject]
  private INoteService NoteService { get; set; } = default!;

  [Parameter]
  public int NoteId { get; set; }
  private NoteResponse? note;
  private string errorMessage = string.Empty;

  protected override async Task OnParametersSetAsync()
  {
    var result = await NoteService.GetNoteByIdAsync(NoteId);

    if (result is { IsSuccessfull: true, Value: not null})
    {
      note = (NoteResponse)result.Value;
    }
    else
    {
      errorMessage = result.ErrorMessage ?? "Lo sentimos, algo salió mal.";
    }
  }
}

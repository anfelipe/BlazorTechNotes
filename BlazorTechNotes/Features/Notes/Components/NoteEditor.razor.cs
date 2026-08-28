using BlazorTechNotes.Application.Features.Notes.Abstractions;
using BlazorTechNotes.Application.Features.Notes.Requests;
using BlazorTechNotes.Infrastructure.Common;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace BlazorTechNotes.Features.Notes.Components;

public partial class NoteEditor
{
  [Inject]
  private INoteService NoteService { get; set; } = default!;

  [Inject]
  private UserManager<User> UserManager { get; set; } = default!;

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;


  [SupplyParameterFromForm]
  private NoteModel? Note { get; set; }

  [Parameter]
  public int? NoteId { get; set; }

  [CascadingParameter]
  private HttpContext HttpContext { get; set; } = default!;

  private bool _isSubmitting;
  private string? _message;
  private bool IsEditMode => NoteId.HasValue;

  protected override async Task OnParametersSetAsync()
  {
    if(NoteId is null)
    {
      Note ??= new();
      return;
    }

    var note = await NoteService.GetNoteByIdAsync(NoteId.Value);

    if (IsEditMode)
    {
      Note ??= note.Value.Adapt<NoteModel>();
    }
    else
    {
      _message = note.Error.Description;
    }
  }

  private async Task HandleValidSubmit()
  {
    _isSubmitting = true;
    _message = null;

    if (IsEditMode)
    {
      var request = Note.Adapt<UpdateNoteRequest>();
      request.Id = NoteId ?? 0;
      var updated = await NoteService.UpdateNoteAsync(request);

      if (!updated.IsSuccess)
      {
        _message = updated.Error.Description;
        _isSubmitting = false;

        return;
      }
      
      Note = updated.Value.Adapt<NoteModel>();
      NavigationManager.NavigateTo("/notes");
    }
    else
    {
      var request = Note.Adapt<CreateNoteRequest>();
      request.UserId = UserManager.GetUserId(HttpContext.User);
      
      var created = await NoteService.CreateNoteAsync(request);

      if (!created.IsSuccess)
      {
        _message = created.Error.Description;
        _isSubmitting = false;
        return;
      }

      Note = created.Value.Adapt<NoteModel>();
      _message = "Nota guardada correctamente.";
    }

    _isSubmitting = false;
  }

  private async Task DeleteNoteAsync()
  {
    _isSubmitting = true;
    _message = null;

    if (IsEditMode)
    {
      var deleted = await NoteService.DeleteNoteAsync(NoteId ?? 0);

      if (!deleted.IsSuccess)
      {
        _message = deleted.Error.Description;

        _isSubmitting = false;
        return;
      }

      _message = "Nota eliminada correctamente.";
      NavigationManager.NavigateTo("/notes");
    }

    _isSubmitting = false;    
  }
  
  private void Reset()
  {
    Note = new NoteModel();
    StateHasChanged();
  }
}

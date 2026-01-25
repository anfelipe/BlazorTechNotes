using BlazorTechNotes.Application.Notes;
using BlazorTechNotes.Infrastructure.Users;
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

  private bool isSubmitting;
  private string? message;
  private bool IsEditMode => NoteId.HasValue;

  protected override async Task OnParametersSetAsync()
  {
    if(NoteId is null)
    {
      Note ??= new();
      return;
    }

    var note = await NoteService.GetNoteByIdAsync(NoteId ?? 0);

    if (IsEditMode)
    {
      Note ??= note.Value.Adapt<NoteModel>();
    }
    else
    {
      message = note.ErrorMessage;
    }
  }

  private async Task HandleValidSubmit()
  {
    isSubmitting = true;
    message = null;

    try
    {
      if (IsEditMode)
      {
        var request = Note.Adapt<UpdateNoteRequest>();
        request.Id = NoteId ?? 0;
        var updated = await NoteService.UpdateNoteAsync(request);

        if (!updated.IsSuccessfull)
        {
          message = updated.ErrorMessage;
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

        if (!created.IsSuccessfull)
        {
          message = created.ErrorMessage;
          return;
        }

        Note = created.Value.Adapt<NoteModel>();
        message = "Nota guardada correctamente.";
      }
    }
    catch (Exception ex)
    {
      message = $"Error al guardar: {ex.Message} - {ex.StackTrace}";
    }
    finally
    {
      isSubmitting = false;
    }
  }

  private async Task DeleteNoteAsync()
  {
    isSubmitting = true;
    message = null;

    try
    {
      if (IsEditMode)
      {
        var deleted = await NoteService.DeleteNoteAsync(NoteId ?? 0);

        if (!deleted.IsSuccessfull)
        {
          message = deleted.ErrorMessage;
          return;
        }

        message = "Nota eliminada correctamente.";
        NavigationManager.NavigateTo("/notes");
      }
    }
    catch (Exception ex)
    {
      message = $"Error al eliminar: {ex.Message} - {ex.StackTrace}";
    }
    finally
    {
      isSubmitting = false;
    }
  }
  
  private void Reset()
  {
    Note = new NoteModel();
    StateHasChanged();
  }
}

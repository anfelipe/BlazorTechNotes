
namespace BlazorTechNotes.Features.Notes;

using System.ComponentModel.DataAnnotations;

public class NoteModel
{
  public int Id { get; set; }

  [Required(ErrorMessage = "El título es obligatorio")]
  [StringLength(200, ErrorMessage = "El título no puede tener más de 200 caracteres")]
  public string Title { get; set; } = string.Empty;

  [StringLength(4000, ErrorMessage = "El contenido no puede exceder 4000 caracteres")]
  public string? Content { get; set; }

  public DateTime? PublishedAt { get; set; }

  public bool IsPublished { get; set; }
  public string UserName { get; set; } = string.Empty;
}

using BlazorTechNotes.Domain.Entities.Notes;
using BlazorTechNotes.Infrastructure.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlazorTechNotes.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext<User>
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
    
  }
  
  public DbSet<Note> Notes { get; set; }

}

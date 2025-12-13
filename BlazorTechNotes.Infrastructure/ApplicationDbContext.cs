using BlazorTechNotes.Domain.Notes;
using BlazorTechNotes.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorTechNotes.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<User>
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
    
  }
  
  public DbSet<Note> Notes { get; set; }

}

using Microsoft.EntityFrameworkCore.Design;

namespace BlazorTechNotes.Infrastructure.Persistence.Contexts;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
  public ApplicationDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    optionsBuilder.UseSqlServer(
      "Server=localhost;Database=TechNotesDb;User ID=SA;Password=MyStrongPass123;TrustServerCertificate=true;MultipleActiveResultSets=true", 
      b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
    );

    return new ApplicationDbContext(optionsBuilder.Options);
  }
}

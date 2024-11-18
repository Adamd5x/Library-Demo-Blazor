using Library.Repository.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public partial class LibraryDbContext (DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging (true);
        optionsBuilder.EnableDetailedErrors (true);
    }

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration (new BookEntityConfiguration ());
    }
}

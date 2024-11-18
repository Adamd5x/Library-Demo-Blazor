using Microsoft.EntityFrameworkCore;

namespace Library.Repository.Seeder;

public class LibrararyDemoDbMigrator(DbContextOptions<LibraryDbContext> options)
{
    private readonly LibraryDbContext context = new(options);

    public async Task MigrateAsync ()
    {
        if (context.Database != null)
        {
            var migrations = await context.Database.GetPendingMigrationsAsync();

            if (migrations is not null)
            {
                await context.Database.MigrateAsync();
            }
        }
    }
}

#nullable enable

using Library.Abstracts;
using Library.Repository.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Repository.Extensions.DependencyInjection;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        string? dbConnectionString = configuration.GetConnectionString("PrimaryDb");

        ArgumentNullException.ThrowIfNull (nameof (dbConnectionString));

        services.AddDbContext<LibraryDbContext> (options => {
            options.UseSqlServer (dbConnectionString);
        });

        services.AddScoped (typeof (IGenericReposiory<>), typeof (GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork> ();

        services.AddScoped<LibrararyDemoDbMigrator > ();

        return services;
    }
}

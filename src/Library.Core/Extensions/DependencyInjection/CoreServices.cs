using System.Reflection;
using Library.Abstracts.Core;
using Library.Core.Services;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Core.Extensions.DependencyInjection;

public static class CoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IStatCoreService, StatCoreService>();
        services.AddScoped<IBookCoreService, BookCoreService>();

        services.AddMappingConfiguration ();

        return services;
    }

    public static IServiceCollection AddMappingConfiguration (this IServiceCollection services) 
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan (Assembly.GetExecutingAssembly ());

        services.AddSingleton (config);
        services.AddScoped<IMapper,  ServiceMapper> ();

        return services;
    }
}

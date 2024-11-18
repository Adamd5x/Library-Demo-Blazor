using Library.Api.Infrastructure;

namespace Library.Api.Extensions.DependencyInjection
{
    public static class GlobalHandlingExtension
    {
        internal static IServiceCollection AddGlobalExceptionHandling (this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler> ();
            services.AddProblemDetails ();

            return services;
        }

        internal static WebApplication UseGlobalExceptionHandling (this WebApplication app)
        {
            app.UseExceptionHandler (_ => { });

            return app;
        }
    }
}

using Library.WebUI.Abstracts;
using Library.WebUI.Services;

namespace Library.WebUI.Extensions.DependencyInjection
{
    public static class AppServicesExtensions
    {
        internal static IServiceCollection AddAppServices (this IServiceCollection services)
        {
            services.AddTransient (typeof (IHttpRepository<>), typeof (HttpRepository<>));

            return services;
        }
    }
}

using Serilog;
using Serilog.Formatting.Json;

namespace Library.Api.Extensions.DependencyInjection
{
    public static class AppExtensions
    {
        public static void SerilogConfiguration (this IHostBuilder host) 
        {
            host.UseSerilog ((context, config) =>
            {
                config.WriteTo.Console (restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose);
                config.WriteTo.File (new JsonFormatter (),
                                     "logs/log-.log",
                                     Serilog.Events.LogEventLevel.Error,
                                     rollingInterval: RollingInterval.Day);
            });
        }
    }
}

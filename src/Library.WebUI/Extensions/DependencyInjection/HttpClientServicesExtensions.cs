using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Library.WebUI.Extensions.DependencyInjection
{
    public static class HttpClientServicesExtensions
    {
        internal static void AddHttpServices (this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped (sp => new HttpClient { BaseAddress = new Uri ("https://localhost:7182") });

            builder.Services.AddScoped (jsonSettings => new JsonSerializerOptions ()
            {
                IgnoreReadOnlyFields = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                MaxDepth = 10
            });


            builder.Services.AddHttpClient ("backend", config =>
            {
                config.BaseAddress = new Uri ("https://localhost:7182");
            }).AddStandardResilienceHandler ();
        }
    }
}

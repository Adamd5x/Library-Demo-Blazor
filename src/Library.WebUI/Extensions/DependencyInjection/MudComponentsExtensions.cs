using MudBlazor;
using MudBlazor.Services;

namespace Library.WebUI.Extensions.DependencyInjection;

internal static class MudComponentsExtensions
{
    internal static IServiceCollection AddMudComponentsServices (this IServiceCollection services)
    {
        services.AddMudServices (config => {
            config.SnackbarConfiguration.PreventDuplicates = true;
            config.SnackbarConfiguration.NewestOnTop = true;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;

            config.PopoverOptions = new ()
            {
                Mode = PopoverMode.Default
            };
        });
        return services;
    }
}

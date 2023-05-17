using GainsTracker.UI.Services;
using Microsoft.Extensions.Logging;

namespace GainsTracker.ClientNative;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        // Register dependencies for injection.
        builder.Services.AddScoped<IGainsAuthService, GainsAuthService>();
        builder.Services.AddScoped<IGainsTrackerService, GainsTrackerService>();
        builder.Services.AddSingleton<HttpClient>();
        
        return builder.Build();
    }
}

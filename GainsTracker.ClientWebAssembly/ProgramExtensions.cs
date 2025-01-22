using GainsTracker.ClientWebAssembly.Auth;
using GainsTracker.Common.Models.Auth;
using GainsTracker.UI.Auth;
using GainsTracker.UI.Services;
using GainsTracker.UI.Services.API;
using GainsTracker.UI.Services.API.Workouts;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;

namespace GainsTracker.ClientWebAssembly;

public static class ProgramExtensions
{
    /// <summary>
    ///     Add authentication and API authorization with Auth0.
    /// </summary>
    public static void ConfigureAuth(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);

        builder.Services.Configure<Auth0ConfigOptions>(options => builder.Configuration.GetSection("Auth0").Bind(options));
        
        builder.Services.AddOidcAuthentication(options =>
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            var auth0Config = serviceProvider.GetRequiredService<IOptions<Auth0ConfigOptions>>().Value;

            builder.Configuration.Bind("Auth0", options.ProviderOptions);
            options.ProviderOptions.ResponseType = "code";
            options.ProviderOptions.AdditionalProviderParameters.Add("audience", auth0Config.Audience);

            options.ProviderOptions.DefaultScopes.Add("openid");
            options.ProviderOptions.DefaultScopes.Add("profile");
            options.ProviderOptions.DefaultScopes.Add("email");
        });

        builder.Services.AddScoped<IAuthService, WebAuthService>();
        
        builder.Services.AddApiAuthorization().AddAccountClaimsPrincipalFactory<UserAccountFactory>();
    }

    /// <summary>
    ///     Configure the HttpClient to retrieve and use JWT's upon logging in.
    /// </summary>
    public static void ConfigureHttpClient(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<ApiService>();
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddScoped(api =>
        {
            var accessTokenProvider = api.GetRequiredService<IAccessTokenProvider>();
            return new ApiService(new HttpClient(new WebAuthMessageHandler(accessTokenProvider)
            { InnerHandler = new HttpClientHandler() })
            { BaseAddress = new Uri("https://localhost:7045/") });
        });
    }

    public static void ConfigureServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IWorkoutService, WorkoutService>();
    }
}

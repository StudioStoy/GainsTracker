using GainsTracker.UI.Services;
using GainsTracker.UI.Services.API;
using GainsTracker.UI.Services.Auth;
using GainsTracker.UI.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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

        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("Auth0", options.ProviderOptions);
            options.ProviderOptions.ResponseType = "code";
            options.ProviderOptions.AdditionalProviderParameters.Add("audience", "https://dev-gainstracker.eu.auth0.com/api/v2/");

            options.ProviderOptions.DefaultScopes.Add("openid");
            options.ProviderOptions.DefaultScopes.Add("profile");
            options.ProviderOptions.DefaultScopes.Add("email");
        });

        
        builder.Services.AddScoped<IGainsAuthService, GainsAuthService>();
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
            { BaseAddress = new Uri("https://localhost:7015/") });
        });
    }
}

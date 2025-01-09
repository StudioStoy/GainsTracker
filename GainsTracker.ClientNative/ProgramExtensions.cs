using GainsTracker.ClientNative.Auth;
using GainsTracker.UI.Auth;
using GainsTracker.UI.Services.API;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace GainsTracker.ClientNative;

public static class ProgramExtensions
{
    /// <summary>
    ///     Add authentication and API authorization with Auth0.
    /// </summary>
    public static void ConfigureAuth(this MauiAppBuilder builder)
    {
        // Auth0 Client Id and domain are both public, and thus can be in source control.
        builder.Services.AddSingleton(new Auth0Client(new Auth0ClientOptions
        {
            Domain = "dev-gainstracker.eu.auth0.com",
            ClientId = "CGofh0U2vV2LF3O593BL38oBlX7GzOly",
            Scope = "openid profile email",
            RedirectUri = "gainstracker://callback",
            RoleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
        }));

        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("Auth0", options.ProviderOptions);
            options.ProviderOptions.ResponseType = "code";
            options.ProviderOptions.AdditionalProviderParameters.Add("audience", "https://dev-gainstracker.eu.auth0.com/api/v2/");
        });

        builder.Services.AddOptions();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddSingleton<AuthenticationStateProvider, Auth0AuthenticationStateProvider>();
        builder.Services.AddApiAuthorization().AddAccountClaimsPrincipalFactory<UserAccountFactory>();
        builder.Services.AddSingleton<IAuthService, NativeAuthService>();
    }

    /// <summary>
    ///     Configure the HttpClient for development and production.
    /// </summary>
    public static void ConfigureHttpClient(this MauiAppBuilder builder)
    {
        builder.Services.AddScoped<HttpClient>();

#if DEBUG
        HttpsClientHandlerService handler = new();
        var platformHandler = handler.GetPlatformMessageHandler();
        var baseAddress = new Uri(DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7045/api/" : "https://localhost:7045/api/");
#else
        var platformHandler = new HttpClientHandler();
        var baseAddress = new Uri("localhost:7045/");
#endif

        builder.Services.AddScoped(sp =>
        {
            var authProvider = sp.GetRequiredService<AuthenticationStateProvider>();

            var authHandler = new NativeAuthMessageHandler(GetToken)
            {
                InnerHandler = platformHandler
            };

            return new ApiService(new HttpClient(authHandler) { BaseAddress = baseAddress });

            async Task<string> GetToken()
            {
                var state = await authProvider.GetAuthenticationStateAsync();
                return state.User.FindFirst(c => c.Type == "access_token")?.Value ?? string.Empty;
            }
        });
    }
}

using System.Reflection;
using GainsTracker.ClientNative.Auth;
using GainsTracker.Common.Models.Auth;
using GainsTracker.UI.Auth;
using GainsTracker.UI.Services.API;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace GainsTracker.ClientNative;

public static class ProgramExtensions
{
    /// <summary>
    ///     Loads the appsettings.json in the root folder.
    /// </summary>
    /// <param name="builder"></param>
    /// <exception cref="InvalidOperationException">Throws when there's no appsettings.json detected</exception>
    public static void ConfigureAppsettings(this MauiAppBuilder builder)
    {
        var asm = Assembly.GetExecutingAssembly();
        var path = $"{asm.GetName().Name}.appsettings.json";
        using Stream stream = asm.GetManifestResourceStream(path) ?? throw new InvalidOperationException();
        builder.Configuration.AddJsonStream(stream);
        builder.Services.AddOptions<Auth0ConfigOptions>().BindConfiguration("Auth0");
    }
    
    /// <summary>
    ///     Add authentication and API authorization with Auth0.
    /// </summary>
    public static void ConfigureAuth(this MauiAppBuilder builder)
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        var auth0Config = serviceProvider.GetRequiredService<IOptions<Auth0ConfigOptions>>().Value;

        builder.Services.AddSingleton(new Auth0Client(new Auth0ClientOptions
        {
            Domain = auth0Config.Authority,
            ClientId = auth0Config.ClientId,
            Scope = "openid profile email",
            RedirectUri = auth0Config.RedirectUri,
            RoleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
        }));

        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("Auth0", options.ProviderOptions);
            options.ProviderOptions.ResponseType = "code";
            options.ProviderOptions.AdditionalProviderParameters.Add("audience", auth0Config.Audience);
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

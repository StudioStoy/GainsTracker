using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace GainsTracker.ClientNative.Auth;

public class Auth0Client(Auth0ClientOptions options)
{
    private readonly OidcClient _oidcClient = new(new OidcClientOptions
    {
        Authority = $"https://{options.Domain}",
        ClientId = options.ClientId,
        Scope = options.Scope,
        RedirectUri = options.RedirectUri,
        Browser = options.Browser,
    });

    public Auth0ClientOptions Options { get; set; } = options;

    public IBrowser Browser
    {
        get => _oidcClient.Options.Browser;
        set => _oidcClient.Options.Browser = value;
    }

    public async Task<LoginResult> LoginAsync()
    {
        return await new Auth0.OidcClient.Auth0Client(new Auth0.OidcClient.Auth0ClientOptions()
        {
            ClientId = Options.ClientId,
            Scope = Options.Scope,
            RedirectUri = Options.RedirectUri,
            Browser = Options.Browser,
            Domain = Options.Domain,
        }).LoginAsync(new { audience = "https://dev-gainstracker.eu.auth0.com/api/v2/" });
    }

    public async Task<BrowserResult> LogoutAsync()
    {
        var logoutParameters = new Dictionary<string, string>
    {
      {"client_id", _oidcClient.Options.ClientId },
      {"returnTo", _oidcClient.Options.RedirectUri }
    };

        var logoutRequest = new LogoutRequest();
        var endSessionUrl = new RequestUrl($"{_oidcClient.Options.Authority}/v2/logout")
          .Create(new Parameters(logoutParameters));
        var browserOptions = new BrowserOptions(endSessionUrl, _oidcClient.Options.RedirectUri)
        {
            Timeout = TimeSpan.FromSeconds(logoutRequest.BrowserTimeout),
            DisplayMode = logoutRequest.BrowserDisplayMode
        };

        var browserResult = await _oidcClient.Options.Browser.InvokeAsync(browserOptions);

        return browserResult;
    }
}

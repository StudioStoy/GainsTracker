namespace GainsTracker.ClientNative.Auth;

public class Auth0ClientOptions
{
    public string Domain { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string RedirectUri { get; set; } = string.Empty;

    public string Scope { get; set; } = string.Empty;

    public string RoleClaim { get; set; } = string.Empty;

    public IdentityModel.OidcClient.Browser.IBrowser Browser { get; set; } = new WebBrowserAuthenticator();
}

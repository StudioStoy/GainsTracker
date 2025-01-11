namespace GainsTracker.Common.Models.Auth;

public class Auth0ConfigOptions
{
    public string Authority { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string RedirectUri { get; set; } = string.Empty;
}

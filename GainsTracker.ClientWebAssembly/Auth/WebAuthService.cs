using GainsTracker.UI.Auth;

namespace GainsTracker.ClientWebAssembly.Auth;

public class WebAuthService() : IAuthService
{
    public Task Login()
    {
        return Task.CompletedTask;
    }

    public Task Logout()
    {
        return Task.CompletedTask;
    }
}

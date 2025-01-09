using GainsTracker.UI.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace GainsTracker.ClientNative.Auth;

public class NativeAuthService(AuthenticationStateProvider authenticationStateProvider) : IAuthService
{
    public async Task Login()
    {
        await ((Auth0AuthenticationStateProvider) authenticationStateProvider)
            .LogInAsync();
    }

    public async Task Logout()
    {
        await ((Auth0AuthenticationStateProvider) authenticationStateProvider)
            .LogOutAsync();
    }
}

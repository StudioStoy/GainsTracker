using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace GainsTracker.ClientNative.Auth;

public class Auth0AuthenticationStateProvider(Auth0Client client) : AuthenticationStateProvider
{
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
        Task.FromResult(new AuthenticationState(_currentUser));

    public Task LogInAsync()
    {
        var loginTask = LogInAsyncCore();
        NotifyAuthenticationStateChanged(loginTask);

        return loginTask;

        async Task<AuthenticationState> LogInAsyncCore()
        {
            var loginResult = await client.LoginAsync();
            if (loginResult.IsError || loginResult.User == null || loginResult.User.Identity == null)
                return new AuthenticationState(new ClaimsPrincipal());

            _currentUser = loginResult.User;
            if (_currentUser.Identity is { IsAuthenticated: true })
                return new AuthenticationState(_currentUser);

            var identity = (ClaimsIdentity)_currentUser.Identity!;
            if (identity.RoleClaimType == client.Options.RoleClaim)
                return new AuthenticationState(_currentUser);

            // Find all roles of the user and re-add them using the correct standard.
            var roleClaims = identity.FindAll(client.Options.RoleClaim).ToArray();
            if (roleClaims.Length != 0)
            {
                foreach (var roleClaim in roleClaims)
                {
                    identity.RemoveClaim(roleClaim);
                }
                foreach (var roleClaim in roleClaims)
                {
                    identity.AddClaim(new Claim(identity.RoleClaimType, roleClaim.Value));
                }
            }

            // Add JWT to the user's claims to send authentication in the web API.
            identity.AddClaim(new Claim("access_token", loginResult.AccessToken));

            return new AuthenticationState(_currentUser);
        }
    }

    public async Task LogOutAsync()
    {
        await client.LogoutAsync();
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }
}

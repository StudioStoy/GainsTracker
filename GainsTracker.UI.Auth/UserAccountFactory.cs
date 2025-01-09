using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace GainsTracker.UI.Auth;

public class UserAccountFactory(IAccessTokenProviderAccessor accessor)
    : AccountClaimsPrincipalFactory<RemoteUserAccount>(accessor)
{
    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(
        RemoteUserAccount account, RemoteAuthenticationUserOptions options)
    {
        var user = await base.CreateUserAsync(account, options);

        if (!(user.Identity?.IsAuthenticated ?? false))
            return user;

        var identity = (ClaimsIdentity) user.Identity;
        account.AdditionalProperties.TryGetValue(ClaimTypes.Role, out var roleClaims);

        if (roleClaims is not JsonElement { ValueKind: JsonValueKind.Array } element)
            return user;

        identity.RemoveClaim(identity.FindFirst(ClaimTypes.Role));

        var claims = element.EnumerateArray()
            .Select(c => new Claim(ClaimTypes.Role, c.ToString()));

        identity.AddClaims(claims);

        return user;
    }
}

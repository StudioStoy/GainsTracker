#region

using GainsTracker.Core.Auth.Interfaces;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace GainsTracker.Infrastructure.Auth;

public static class AuthServiceCollections
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}

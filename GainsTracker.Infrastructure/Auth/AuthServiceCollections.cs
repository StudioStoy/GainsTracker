using System;
using GainsTracker.Core.Auth.Interfaces;
using Microsoft.Extensions.DependencyInjection;

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

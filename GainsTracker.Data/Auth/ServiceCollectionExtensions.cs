using GainsTracker.Core.Security.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Data.Auth;

public static class ServerCollectionExtensions
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        return services;
    }
}

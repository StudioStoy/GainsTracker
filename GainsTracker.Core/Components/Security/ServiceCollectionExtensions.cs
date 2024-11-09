using GainsTracker.Core.Components.Security.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Core.Components.Security;

public static class ServerCollectionExtensions
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        return services;
    }
}

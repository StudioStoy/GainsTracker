using GainsTracker.Core.Security.Services;
using GainsTracker.Core.Workouts.Models;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Core.Security;

public static class ServerCollectionExtensions
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        return services;
    }
}

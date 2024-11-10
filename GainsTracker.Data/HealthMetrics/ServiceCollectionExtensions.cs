using GainsTracker.Core.HealthMetrics.Interfaces.Repositories;
using GainsTracker.Core.HealthMetrics.Interfaces.Services;
using GainsTracker.Core.HealthMetrics.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Data.HealthMetrics;

public static class ServerCollectionExtensions
{
    public static IServiceCollection AddHealthMetricServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IHealthMetricService, HealthMetricService>();
        services.AddScoped<IHealthMetricBigBrain, HealthMetricBigBrain>();
        
        return services;
    }
}

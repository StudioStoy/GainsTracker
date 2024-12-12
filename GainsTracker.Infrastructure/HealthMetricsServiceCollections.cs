using System;
using GainsTracker.Core.HealthMetrics.Interfaces.Repositories;
using GainsTracker.Core.HealthMetrics.Interfaces.Services;
using GainsTracker.Core.HealthMetrics.Services;
using GainsTracker.Data.HealthMetrics;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Infrastructure;

public static class HealthMetricsServiceCollections
{
    public static IServiceCollection AddHealthMetricServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IHealthMetricService, HealthMetricService>();
        services.AddScoped<IHealthMetricRepository, HealthMetricRepository>();

        return services;
    }
}

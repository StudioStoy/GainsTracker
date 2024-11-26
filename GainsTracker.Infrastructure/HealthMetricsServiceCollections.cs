using GainsTracker.Core;
using GainsTracker.Core.HealthMetrics.Interfaces.Repositories;
using GainsTracker.Core.HealthMetrics.Interfaces.Services;
using GainsTracker.Core.HealthMetrics.Models;
using GainsTracker.Core.HealthMetrics.Services;
using GainsTracker.Data;
using GainsTracker.Data.HealthMetrics;
using GainsTracker.Data.HealthMetrics.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Infrastructure;

public static class HealthMetricsServiceCollections
{
    public static IServiceCollection AddHealthMetricServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IHealthMetricService, HealthMetricService>();
        services.AddScoped<IHealthMetricBigBrain, HealthMetricRepository>();
        services.AddTransient(typeof(IGenericRepository<HealthMetric>),
            typeof(GenericRepository<HealthMetric, HealthMetricEntity>));

        return services;
    }
}

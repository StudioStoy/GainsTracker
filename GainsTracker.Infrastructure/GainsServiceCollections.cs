using System;
using GainsTracker.Core.Gains.Interfaces.Repositories;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Gains.Services;
using GainsTracker.Data.Gains;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Infrastructure;

public static class GainsServiceCollections
{
    public static IServiceCollection AddGainsServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IGainsRepository, GainsRepository>();
        services.AddScoped<IGainsService, GainsService>();

        return services;
    }
}

using GainsTracker.Core;
using GainsTracker.Core.Gains.Interfaces.Repositories;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Gains.Models;
using GainsTracker.Core.Gains.Services;
using GainsTracker.Data;
using GainsTracker.Data.Gains;
using GainsTracker.Data.Gains.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Infrastructure;

public static class GainsServiceCollections
{
    public static IServiceCollection AddGainsServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IGainsBigBrain, GainsRepository>();
        services.AddScoped<IGainsService, GainsService>();
        services.AddTransient(typeof(IGenericRepository<GainsAccount>),
            typeof(GenericRepository<GainsAccount, GainsAccountEntity>));

        return services;
    }
}

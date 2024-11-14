﻿using GainsTracker.Core;
using GainsTracker.Core.Gains.Interfaces.Repositories;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Gains.Services;
using GainsTracker.Data.Gains.Entities;
using GainsTracker.Data.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Data.Gains;

public static class ServerCollectionExtensions
{
    public static IServiceCollection AddGainsServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        
        services.AddScoped<IBigBrain<GainsAccountEntity>, BigBrain<GainsAccountEntity>>();
        services.AddScoped<IGainsService, GainsService>();
        services.AddScoped<IGainsBigBrain, GainsBigBrain>();

        return services;
    }
}

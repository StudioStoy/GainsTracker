using GainsTracker.Core;
using GainsTracker.Core.Workouts.Models;
using GainsTracker.Data.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Data.Gains;

public static class ServerCollectionExtensions
{
    public static IServiceCollection AddGainsServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IBigBrain<GainsAccount>, BigBrain<GainsAccount>>();

        Console.WriteLine("hello?");
        return services;
    }
}

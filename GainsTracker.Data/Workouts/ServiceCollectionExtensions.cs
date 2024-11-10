using GainsTracker.Core.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Workouts.Interfaces.Services;
using GainsTracker.Core.Workouts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Data.Workouts;

public static class ServerCollectionExtensions
{
    public static IServiceCollection AddWorkoutServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IWorkoutService, WorkoutService>();
        services.AddScoped<IWorkoutBigBrain, WorkoutBigBrain>();
        services.AddScoped<ICatalogService, CatalogService>();
        services.AddScoped<IMeasurementValidationService, MeasurementValidationService>();
        
        return services;
    }
}

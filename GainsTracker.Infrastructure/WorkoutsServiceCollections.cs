using GainsTracker.Core.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Workouts.Interfaces.Services;
using GainsTracker.Core.Workouts.Services;
using GainsTracker.Data.Workouts;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Infrastructure;

public static class WorkoutsServiceCollections
{
    public static IServiceCollection AddWorkoutServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IWorkoutService, WorkoutService>();
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        services.AddScoped<IMeasurementRepository, MeasurementRepository>();
        services.AddScoped<ICatalogService, CatalogService>();
        services.AddScoped<IMeasurementValidationService, MeasurementValidationService>();

        return services;
    }
}

using GainsTracker.Core;
using GainsTracker.Core.Workouts.Interfaces.Repositories;
using GainsTracker.Core.Workouts.Interfaces.Services;
using GainsTracker.Core.Workouts.Models.Measurements;
using GainsTracker.Core.Workouts.Models.Workouts;
using GainsTracker.Core.Workouts.Services;
using GainsTracker.Data;
using GainsTracker.Data.Workouts;
using GainsTracker.Data.Workouts.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Infrastructure;

public static class WorkoutsServiceCollections
{
    public static IServiceCollection AddWorkoutServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IWorkoutService, WorkoutService>();
        services.AddScoped<IWorkoutBigBrain, WorkoutRepository>();
        services.AddScoped<ICatalogService, CatalogService>();
        services.AddScoped<IMeasurementValidationService, MeasurementValidationService>();
        services.AddTransient(typeof(IGenericRepository<Workout>), typeof(GenericRepository<Workout, WorkoutEntity>));
        services.AddTransient(typeof(IGenericRepository<Measurement>),
            typeof(GenericRepository<Measurement, MeasurementEntity>));

        return services;
    }
}

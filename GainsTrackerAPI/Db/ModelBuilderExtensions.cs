using GainsTrackerAPI.Gains.Models;
using GainsTrackerAPI.Gains.Models.Measurements;
using Microsoft.EntityFrameworkCore;

namespace GainsTrackerAPI.Db;

public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Converts the enum names in the database to string format.
    ///     Add any new enums here to make sure they get converted properly
    /// </summary>
    public static void ConvertEnumsToStrings(this ModelBuilder builder)
    {
        // Workout types
        builder.Entity<Workout>()
            .Property(workout => workout.WorkoutType)
            .HasConversion<string>();

        // Measurement types
        builder.Entity<StrengthMeasurement>()
            .Property(measurement => measurement.WeightUnit)
            .HasConversion<string>();

        builder.Entity<RunningEnduranceMeasurement>()
            .Property(measurement => measurement.DistanceUnit)
            .HasConversion<string>();

        builder.Entity<RunningEnduranceMeasurement>()
            .Property(measurement => measurement.TimeUnit)
            .HasConversion<string>();

        builder.Entity<SimpleEnduranceMeasurement>()
            .Property(measurement => measurement.TimeUnit)
            .HasConversion<string>();
    }
}

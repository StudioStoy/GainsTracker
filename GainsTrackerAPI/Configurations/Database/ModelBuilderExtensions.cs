using GainsTrackerAPI.Gains.Models;
using GainsTrackerAPI.Gains.Models.Friends;
using GainsTrackerAPI.Gains.Models.Measurements;
using Microsoft.EntityFrameworkCore;

namespace GainsTrackerAPI.Db;

public static class ModelBuilderExtensions
{
    public static void ConfigureRelationModels(this ModelBuilder builder)
    {
        builder.Entity<FriendRequest>()
            .HasOne(a => a.RequestedBy)
            .WithMany(b => b.SentFriendRequests)
            .HasForeignKey(f => f.RequestedById);

        builder.Entity<FriendRequest>()
            .HasOne(a => a.RequestedTo)
            .WithMany(b => b.ReceivedFriendRequests)
            .HasForeignKey(c => c.RequestedToId);
    }

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

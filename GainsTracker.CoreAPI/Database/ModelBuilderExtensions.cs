using GainsTracker.CoreAPI.Components.Friends.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.CoreAPI.Database;

public static class ModelBuilderExtensions
{
    public static void ConfigureRelationModels(this ModelBuilder builder)
    {
        builder.Entity<FriendRequest>()
            .HasOne(a => a.Requester)
            .WithMany(b => b.SentFriendRequests)
            .HasForeignKey(f => f.RequesterId);

        builder.Entity<FriendRequest>()
            .HasOne(a => a.Recipient)
            .WithMany(b => b.ReceivedFriendRequests)
            .HasForeignKey(c => c.RecipientId);
    }

    /// <summary>
    ///     Converts the enum names in the database to string format.
    ///     Add any new enums here to make sure they get converted properly
    /// </summary>
    public static void ConvertEnumsToStrings(this ModelBuilder builder)
    {
        // Workout types
        builder.Entity<Workout>()
            .Property(workout => workout.Type)
            .HasConversion<string>();

        // Measurement types
        builder.Entity<StrengthMeasurement>()
            .Property(measurement => measurement.WeightUnit)
            .HasConversion<string>();

        builder.Entity<TimeAndDistanceEnduranceMeasurement>()
            .Property(measurement => measurement.DistanceUnit)
            .HasConversion<string>();
    }
}

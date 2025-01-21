using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.Gains.Models;
using GainsTracker.Core.UserProfiles.Models;
using GainsTracker.Core.Workouts.Models.Measurements;
using GainsTracker.Core.Workouts.Models.Workouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GainsTracker.Data;

public static class ModelBuilderExtensions
{
    public static void ConfigureRelationModels(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FriendRequest>()
            .HasOne(a => a.Requester)
            .WithMany(b => b.SentFriendRequests)
            .HasForeignKey(f => f.RequesterId);

        modelBuilder.Entity<FriendRequest>()
            .HasOne(a => a.Recipient)
            .WithMany(b => b.ReceivedFriendRequests)
            .HasForeignKey(c => c.RecipientId);

        modelBuilder.Entity<GainsAccount>(gainsAccount =>
        {
            gainsAccount.Navigation(g => g.SentFriendRequests).AutoInclude();
            gainsAccount.Navigation(g => g.ReceivedFriendRequests).AutoInclude();
        });

        // Measurements
        modelBuilder.Entity<Measurement>()
            .ToTable("Measurements");

        modelBuilder.Entity<StrengthMeasurement>()
            .ToTable("StrengthMeasurements");

        modelBuilder.Entity<TimeAndDistanceEnduranceMeasurement>()
            .ToTable("TimeAndDistanceEnduranceMeasurements");

        modelBuilder.Entity<TimeEnduranceMeasurement>()
            .ToTable("TimeEnduranceMeasurements");

        modelBuilder.Entity<RepsMeasurement>()
            .ToTable("RepsMeasurements");

        modelBuilder.Entity<GeneralMeasurement>()
            .ToTable("GeneralMeasurements");
    }

    /// <summary>
    ///     Converts the enum names in the database to string format.
    ///     Add any new enum classes here to make sure they get converted properly
    /// </summary>
    public static void ConvertEnumsToStrings(this ModelBuilder modelBuilder)
    {
        // Workout types
        modelBuilder.Entity<Workout>()
            .Property(workout => workout.Type)
            .HasConversion<string>();

        // Measurement types
        modelBuilder.Entity<StrengthMeasurement>()
            .Property(measurement => measurement.WeightUnit)
            .HasConversion<string>();

        modelBuilder.Entity<TimeAndDistanceEnduranceMeasurement>()
            .Property(measurement => measurement.DistanceUnit)
            .HasConversion<string>();
    }

    /// <summary>
    ///     Converts other properties to the correct database format.
    /// </summary>
    public static void ConvertCustomPropertiesToDbFormat(this ModelBuilder builder)
    {
        var timeConverter = new ValueConverter<long, string>(v => v.ToString(), v => Convert.ToInt64(v));

        builder.Entity<TimeEnduranceMeasurement>()
            .Property(measurement => measurement.Time)
            .HasConversion(timeConverter);

        builder.Entity<TimeAndDistanceEnduranceMeasurement>()
            .Property(measurement => measurement.Time)
            .HasConversion(timeConverter);
    }
}

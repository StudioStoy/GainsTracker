using GainsTracker.Core.Friends.Models;
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

        modelBuilder.Entity<UserProfile>(userProfile =>
        {
            userProfile
                .HasMany(u => u.PinnedPBs)
                .WithOne()
                .HasForeignKey(u => u.PinnedByUserProfileId)
                .IsRequired(false);
            
            userProfile
                .Navigation(u => u.Icon)
                .AutoInclude();
        });

        // Measurements
        modelBuilder.Entity<Measurement>()
            .ToTable("measurements");

        modelBuilder.Entity<StrengthMeasurement>()
            .ToTable("strength_measurements");

        modelBuilder.Entity<TimeDistanceEnduranceMeasurement>()
            .ToTable("time_distance_endurance_measurements");

        modelBuilder.Entity<TimeEnduranceMeasurement>()
            .ToTable("time_endurance_measurements");

        modelBuilder.Entity<RepsMeasurement>()
            .ToTable("reps_measurements");

        modelBuilder.Entity<GeneralMeasurement>()
            .ToTable("general_measurements");
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

        modelBuilder.Entity<TimeDistanceEnduranceMeasurement>()
            .Property(measurement => measurement.DistanceUnit)
            .HasConversion<string>();
    }
}

using GainsTracker.Data.Friends.Entities;
using GainsTracker.Data.Gains.Entities;
using GainsTracker.Data.UserProfiles.Entities;
using GainsTracker.Data.Workouts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GainsTracker.Data;

public static class ModelBuilderExtensions
{
    public static void ConfigureRelationModels(this ModelBuilder builder)
    {
        builder.Entity<FriendRequestEntity>()
            .HasOne(a => a.Requester)
            .WithMany(b => b.SentFriendRequests)
            .HasForeignKey(f => f.RequesterId);

        builder.Entity<FriendRequestEntity>()
            .HasOne(a => a.Recipient)
            .WithMany(b => b.ReceivedFriendRequests)
            .HasForeignKey(c => c.RecipientId);

        builder.Entity<UserEntity>(user =>
        {
            user.HasOne(u => u.GainsAccount)
                .WithOne()
                .HasForeignKey<UserEntity>(u => u.GainsAccountId);
        });

        builder.Entity<GainsAccountEntity>(gainsAccount =>
        {
            gainsAccount.HasOne(u => u.UserProfile)
                .WithOne()
                .HasForeignKey<GainsAccountEntity>(u => u.UserProfileId);

            gainsAccount.Navigation(g => g.SentFriendRequests).AutoInclude();
            gainsAccount.Navigation(g => g.ReceivedFriendRequests).AutoInclude();
        });

        builder.Entity<UserProfileEntity>(userProfile =>
        {
            userProfile.HasMany(u => u.PinnedPBs)
                .WithOne()
                .HasForeignKey(u => u.UserProfileId)
                .IsRequired(false);

            userProfile.HasOne(u => u.Icon)
                .WithOne()
                .HasForeignKey<ProfileIconEntity>(i => i.UserProfileId);
        });
    }

    /// <summary>
    ///     Converts the enum names in the database to string format.
    ///     Add any new enum classes here to make sure they get converted properly
    /// </summary>
    public static void ConvertEnumsToStrings(this ModelBuilder builder)
    {
        // WorkoutEntity types
        builder.Entity<WorkoutEntity>()
            .Property(workout => workout.Type)
            .HasConversion<string>();

        // Measurement types
        builder.Entity<StrengthMeasurementEntity>()
            .Property(measurement => measurement.WeightUnit)
            .HasConversion<string>();

        builder.Entity<TimeAndDistanceEnduranceMeasurementEntity>()
            .Property(measurement => measurement.DistanceUnit)
            .HasConversion<string>();
    }

    /// <summary>
    ///     Converts other properties to the correct database format.
    /// </summary>
    public static void ConvertCustomPropertiesToDbFormat(this ModelBuilder builder)
    {
        var timeConverter = new ValueConverter<long, string>(v => v.ToString(), v => Convert.ToInt64(v));

        builder.Entity<TimeEnduranceMeasurementEntity>()
            .Property(measurement => measurement.Time)
            .HasConversion(timeConverter);

        builder.Entity<TimeAndDistanceEnduranceMeasurementEntity>()
            .Property(measurement => measurement.Time)
            .HasConversion(timeConverter);
    }
}
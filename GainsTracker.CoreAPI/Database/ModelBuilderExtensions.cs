using GainsTracker.CoreAPI.Components.Friends.Models;
using GainsTracker.CoreAPI.Components.Security.Models;
using GainsTracker.CoreAPI.Components.UserProfiles.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
        
        builder.Entity<User>(user =>
        {
            user.HasOne(u => u.GainsAccount)
                .WithOne()
                .HasForeignKey<User>(u => u.GainsAccountId);
        });
        
        builder.Entity<GainsAccount>(gainsAccount =>
        {
            gainsAccount.HasOne(u => u.UserProfile)
                .WithOne()
                .HasForeignKey<GainsAccount>(u => u.UserProfileId);

            gainsAccount.Navigation(g => g.SentFriendRequests).AutoInclude();
            gainsAccount.Navigation(g => g.ReceivedFriendRequests).AutoInclude();
        });
        
        builder.Entity<UserProfile>(userProfile =>
        {
            userProfile.HasMany(u => u.PinnedPBs)
                .WithOne()
                .HasForeignKey(u => u.UserProfileId)
                .IsRequired(false);

            userProfile.HasOne(u => u.Icon)
                .WithOne();
        });
    }

    /// <summary>
    ///     Converts the enum names in the database to string format.
    ///     Add any new enum classes here to make sure they get converted properly
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

using System.Data.Entity.ModelConfiguration.Conventions;
using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.Gains.Models;
using GainsTracker.Core.HealthMetrics.Models;
using GainsTracker.Core.UserProfiles.Models;
using GainsTracker.Core.Users.Models;
using GainsTracker.Core.Workouts.Models.Measurements;
using GainsTracker.Core.Workouts.Models.Workouts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GainsTracker.Data;

public sealed class GainsDbContext(DbContextOptions<GainsDbContext> options) : DbContext(options)
{
    // The main domains.
    public DbSet<User> Users { get; set; }
    public DbSet<GainsAccount> GainsAccounts { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<ProfileIcon> ProfileIcons { get; set; }

    // Measurements and its derived classes.
    public DbSet<Measurement> Measurements { get; set; }
    public DbSet<RepsMeasurement> SimpleRepMeasurements { get; set; }
    public DbSet<TimeEnduranceMeasurement> SimpleEnduranceMeasurements { get; set; }
    public DbSet<TimeDistanceEnduranceMeasurement> RunningMeasurements { get; set; }
    public DbSet<StrengthMeasurement> WeightMeasurements { get; set; }
    public DbSet<GeneralMeasurement> GeneralMeasurements { get; set; }

    // Metrics and its derived classes.
    public DbSet<HealthMetric> Metrics { get; set; }
    public DbSet<WeightHealthMetric> WeightMetrics { get; set; }
    public DbSet<ProteinHealthMetric> ProteinMetrics { get; set; }
    public DbSet<LiterWaterHealthMetric> LiterWaterMetrics { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove(typeof(OneToManyCascadeDeleteConvention));
        configurationBuilder.Conventions.Remove(typeof(ManyToManyCascadeDeleteConvention));
    }

    // In here, all the many-to-one, one-to-one, etc. relations are managed.
    // Also, auto-includes.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureRelationModels();
        modelBuilder.ConvertEnumsToStrings();
        modelBuilder.ConvertCustomPropertiesToDbFormat();

        foreach (var item in modelBuilder.Model.GetEntityTypes())
        {
            var p = item.FindPrimaryKey()?.Properties.FirstOrDefault(i => i.ValueGenerated != ValueGenerated.Never);
            if (p != null) p.ValueGenerated = ValueGenerated.Never;
        }

        modelBuilder.Entity<FriendRequest>().Property(e => e.RequesterId).ValueGeneratedNever();
        modelBuilder.Entity<FriendRequest>().Property(e => e.Id).ValueGeneratedNever();
        modelBuilder.Entity<FriendRequest>().Property(e => e.RecipientId).ValueGeneratedNever();

        // new DbInitializer(modelBuilder).Seed();

        base.OnModelCreating(modelBuilder);
    }
}

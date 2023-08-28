using System.Data.Entity.ModelConfiguration.Conventions;
using GainsTracker.CoreAPI.Components.Friends.Models;
using GainsTracker.CoreAPI.Components.HealthMetrics.Models;
using GainsTracker.CoreAPI.Components.Security.Models;
using GainsTracker.CoreAPI.Components.UserProfiles.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.CoreAPI.Database;

public sealed class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public override DbSet<User> Users { get; set; }
    public DbSet<GainsAccount> GainsAccounts { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<FriendRequest> FriendRequests { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<ProfileIcon> ProfileIcons { get; set; }

    // Measurements and its derived classes.
    public DbSet<Measurement> Measurements { get; set; }
    public DbSet<RepsMeasurement> SimpleRepMeasurements { get; set; }
    public DbSet<TimeEnduranceMeasurement> SimpleEnduranceMeasurements { get; set; }
    public DbSet<TimeAndDistanceEnduranceMeasurement> RunningMeasurements { get; set; }
    public DbSet<StrengthMeasurement> WeightMeasurements { get; set; }
    public DbSet<GeneralMeasurement> GeneralMeasurements { get; set; }

    // Metrics and its derived classes.
    public DbSet<Metric> Metrics { get; set; }
    public DbSet<WeightMetric> WeightMetrics { get; set; }
    public DbSet<ProteinMetric> ProteinMetrics { get; set; }
    public DbSet<LiterWaterMetric> LiterWaterMetrics { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove(typeof(OneToManyCascadeDeleteConvention));
        configurationBuilder.Conventions.Remove(typeof(ManyToManyCascadeDeleteConvention));
    }

    // In here, all the many-to-one, one-to-one, etc relations are managed.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureRelationModels();
        modelBuilder.ConvertEnumsToStrings();

        modelBuilder.Entity<User>()
            .HasOne(u => u.GainsAccount)
            .WithOne()
            .HasForeignKey<User>(u => u.GainsAccountId);

        modelBuilder.Entity<GainsAccount>()
            .HasOne(u => u.UserProfile)
            .WithOne()
            .HasForeignKey<GainsAccount>(u => u.UserProfileId);

        modelBuilder.Entity<UserProfile>()
            .HasMany(u => u.PinnedPBs)
            .WithOne()
            .HasForeignKey(u => u.UserProfileId)
            .IsRequired(false);

        modelBuilder.Entity<UserProfile>()
            .HasOne(u => u.Icon)
            .WithOne();

        new DbInitializer(modelBuilder).Seed();

        base.OnModelCreating(modelBuilder);
    }

    // EF automatically creates tables using their camelcase name.
    // We don't want this in postgres as this necessitates stupid queries, like:
    // 'select * from public."Workout"' instead of 'select * from workout'.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql()
            .UseSnakeCaseNamingConvention();
    }
}

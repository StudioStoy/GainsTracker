using System.Data.Entity.ModelConfiguration.Conventions;
using GainsTracker.Core.Security.Models;
using GainsTracker.Data.Friends.Entities;
using GainsTracker.Data.Gains.Entities;
using GainsTracker.Data.HealthMetrics.Entities;
using GainsTracker.Data.UserProfiles.Entities;
using GainsTracker.Data.Workouts.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.Shared;

public sealed class GainsDbContext : IdentityDbContext<User>
{
    public GainsDbContext(DbContextOptions<GainsDbContext> options) : base(options)
    {
    }
    
    // The main domains.
    public override DbSet<User> Users { get; set; }
    public DbSet<GainsAccountEntity> GainsAccounts { get; set; }
    public DbSet<WorkoutEntity> Workouts { get; set; }
    public DbSet<FriendEntity> Friends { get; set; }
    public DbSet<FriendRequestEntity> FriendRequests { get; set; }
    public DbSet<UserProfileEntity> UserProfiles { get; set; }
    public DbSet<ProfileIconEntity> ProfileIcons { get; set; }

    // Measurements and its derived classes.
    public DbSet<MeasurementEntity> Measurements { get; set; }
    public DbSet<RepsMeasurementEntity> SimpleRepMeasurements { get; set; }
    public DbSet<TimeEnduranceMeasurementEntity> SimpleEnduranceMeasurements { get; set; }
    public DbSet<TimeAndDistanceEnduranceMeasurementEntity> RunningMeasurements { get; set; }
    public DbSet<StrengthMeasurementEntity> WeightMeasurements { get; set; }
    public DbSet<GeneralMeasurementEntity> GeneralMeasurements { get; set; }

    // Metrics and its derived classes.
    public DbSet<HealthMetricEntity> Metrics { get; set; }
    public DbSet<WeightHealthMetricEntity> WeightMetrics { get; set; }
    public DbSet<ProteinHealthMetricEntity> ProteinMetrics { get; set; }
    public DbSet<LiterWaterHealthMetricEntity> LiterWaterMetrics { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove(typeof(OneToManyCascadeDeleteConvention));
        configurationBuilder.Conventions.Remove(typeof(ManyToManyCascadeDeleteConvention));
    }

    // In here, all the many-to-one, one-to-one, etc relations are managed.
    // Also auto-includes.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureRelationModels();
        modelBuilder.ConvertEnumsToStrings();
        modelBuilder.ConvertCustomPropertiesToDbFormat();
        
        // new DbInitializer(modelBuilder).Seed();

        base.OnModelCreating(modelBuilder);
    }

    // EF automatically creates tables using their camelcase name.
    // I don't want this in postgres as this necessitates stupid queries, like:
    // 'select * from public."WorkoutEntity"' instead of 'select * from workout'.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql()
            .UseSnakeCaseNamingConvention();
    }
}

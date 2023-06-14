using System.Data.Entity.ModelConfiguration.Conventions;
using GainsTracker.CoreAPI.Components.Friends.Models;
using GainsTracker.CoreAPI.Components.HealthMetrics.Models;
using GainsTracker.CoreAPI.Components.Security.Models;
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
    public DbSet<FriendRequest> FriendRequests { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Measurement> Measurements { get; set; }
    public DbSet<Metric> Metrics { get; set; }

    // Derived classes of Measurement
    public DbSet<RepsMeasurement> SimpleRepMeasurements { get; set; }
    public DbSet<TimeEnduranceMeasurement> SimpleEnduranceMeasurements { get; set; }
    public DbSet<TimeAndDistanceEnduranceMeasurement> RunningMeasurements { get; set; }
    public DbSet<StrengthMeasurement> WeightMeasurements { get; set; }

    // Derived classes of Metric
    public DbSet<WeightMetric> WeightMetrics { get; set; }
    public DbSet<ProteinMetric> ProteinMetrics { get; set; }

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

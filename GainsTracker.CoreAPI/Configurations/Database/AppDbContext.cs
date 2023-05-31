using System.Data.Entity.ModelConfiguration.Conventions;
using GainsTracker.CoreAPI.Components.Friend.Models;
using GainsTracker.CoreAPI.Components.Security.Models;
using GainsTracker.CoreAPI.Components.Workout.Models;
using GainsTracker.CoreAPI.Components.Workout.Models.Measurements;
using GainsTracker.CoreAPI.Components.Workout.Models.Workouts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.CoreAPI.Configurations.Database;

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

    // Derived classes of Measurement
    public DbSet<SimpleRepMeasurement> SimpleRepMeasurements { get; set; }
    public DbSet<SimpleEnduranceMeasurement> SimpleEnduranceMeasurements { get; set; }
    public DbSet<RunningEnduranceMeasurement> RunningMeasurements { get; set; }
    public DbSet<StrengthMeasurement> WeightMeasurements { get; set; }

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

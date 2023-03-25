using GainsTrackerAPI.Gains.Models;
using GainsTrackerAPI.Security.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GainsTrackerAPI.Db;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<GainsAccount> GainsAccounts { get; set; }
    public DbSet<Workout> Workouts { get; set; }

    // In here, all the many-to-one, one-to-one, etc relations are managed.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GainsAccount>()
            .HasOne(g => g.User)
            .WithOne(u => u.GainsAccount);

        modelBuilder.Entity<Workout>()
            .HasOne(w => w.GainsAccount)
            .WithMany(g => g.Workouts);

        new DbInitializer(modelBuilder).Seed();

        base.OnModelCreating(modelBuilder);
    }

    // EF automatically creates tables using their camelcase name.
    // We don't want this in postgres as this creates stupid queries, like:
    // 'select * from public."Workout"' instead of 'select * from workout'.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql()
            .UseLowerCaseNamingConvention();
    }
}

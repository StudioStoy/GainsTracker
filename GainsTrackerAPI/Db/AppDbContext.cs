using GainsTrackerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GainsTrackerAPI.Db;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
        const string ROLE_ID = ADMIN_ID;
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = ROLE_ID,
            Name = "admin",
            NormalizedName = "ADMIN"
        });
        
        var user = new User();
        var hasher = new PasswordHasher<User>();

        // When creating a default user, it is necessary to fill in the normalized fields as well as the security stamp.
        var admin = new User
        {
            Id = ADMIN_ID,
            UserName = "stije",
            NormalizedUserName = "STIJE",
            Email = "stije@studiostoy.nl",
            EmailConfirmed = true,
            NormalizedEmail = "STIJE@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, "admin"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        modelBuilder.Entity<User>().HasData(admin);
        
        Database.MigrateAsync();
    }
}

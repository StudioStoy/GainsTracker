using GainsTrackerAPI.Gains.Models;
using GainsTrackerAPI.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GainsTrackerAPI.Db;

public class DbInitializer
{
    private readonly ModelBuilder _builder;

    public DbInitializer(ModelBuilder builder)
    {
        _builder = builder;
    }

    public void Seed()
    {
        const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
        const string GAINSACCOUNT_ID = "e58eddff-d5be-46c1-9c99-1283d54152d1";
        const string ROLE_ID = ADMIN_ID;
        _builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = ROLE_ID,
            Name = "admin",
            NormalizedName = "ADMIN"
        });

        User user = new();
        PasswordHasher<User> hasher = new();

        // When creating a default user, it is necessary to fill in the normalized fields as well as the security stamp.
        User admin = new()
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

        _builder.Entity<User>(a => { a.HasData(admin); });

        _builder.Entity<GainsAccount>(g =>
        {
            g.HasData(new GainsAccount
            {
                Id = GAINSACCOUNT_ID,
                UserId = ADMIN_ID,
                UserName = admin.UserName
            });
        });

        _builder.Entity<Workout>(g =>
        {
            g.HasData(new Workout
            {
                GainsAccountId = GAINSACCOUNT_ID,
                Type = "pushups",
                PersonalBest = 6
            });
        });
    }
}

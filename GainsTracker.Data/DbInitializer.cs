using DotNetEnv;
using GainsTracker.Core.Auth.Models;
using GainsTracker.Core.Gains.Models;
using GainsTracker.Core.UserProfiles.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data;

public class DbInitializer(ModelBuilder builder)
{
    // Seeding will only work after an initial migration has been done.
    public void Seed()
    {
        Console.WriteLine("If applicable, seeding db..");

        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
        Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
        Name = "admin",
        NormalizedName = "ADMIN"
        });
        
        User user = new("");
        PasswordHasher<User> hasher = new();
        
        // When creating a default user, it is necessary to fill in the normalized fields as well as the security stam
        User defaultUser = new("stije")
        {
        Id = CreateId(),
        NormalizedUserName = "STIJE",
        Email = "stije@studiostoy.nl",
        EmailConfirmed = true,
        NormalizedEmail = "STIJE@STUDIOSTOY.NL",
        PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "adminn"),
        SecurityStamp = CreateId()
        };
        
        string display = "DavrozzGaining";
        var gains = new GainsAccount(defaultUser.UserName!, display)
        {
            Workouts = []
        };
        defaultUser.GainsAccountId = gains.Id;
             
        gains.UserProfile = null!;
        var icon = new ProfileIcon();
        var profile = new UserProfile(gains.Id)
        {
            Icon = icon
        };

        builder.Entity<GainsAccount>(gainsTable => gainsTable.HasData(gains));
        builder.Entity<User>(userTable => userTable.HasData(defaultUser));
        builder.Entity<UserProfile>(userTable => userTable.HasData(profile));
        builder.Entity<ProfileIcon>(userTable => userTable.HasData(icon));
    }

    private static string CreateId() => Guid.NewGuid().ToString();
}

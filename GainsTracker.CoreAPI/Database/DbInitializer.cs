using DotNetEnv;
using GainsTracker.CoreAPI.Components.Security.Models;
using GainsTracker.CoreAPI.Components.UserProfiles.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.CoreAPI.Database;

public class DbInitializer
{
    private readonly ModelBuilder _builder;

    public DbInitializer(ModelBuilder builder)
    {
        _builder = builder;
    }

    // Seeding will only work after an initial migration has been done.
    public void Seed()
    {
        Console.WriteLine("seeding db..");
        
        _builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
            Name = "admin",
            NormalizedName = "ADMIN"
        });

        User user = new("");
        PasswordHasher<User> hasher = new();

        // When creating a default user, it is necessary to fill in the normalized fields as well as the security stam
        User stije = new("stije")
        {
            Id = CreateId(),
            NormalizedUserName = "STIJE",
            Email = "stije@studiostoy.nl",
            EmailConfirmed = true,
            NormalizedEmail = "STIJE@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "adminn"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        List<User> defaultUsers = new() { stije };
        
        foreach (var username in new string[] { "joyo","bino","soep","eef","jordt","sanda","naoh","dyllo","arv","japser"})
        {
            User newUser = new(username)
            {
                Id = CreateId(),
                NormalizedUserName = username.ToUpper(),
                Email = $"{username}@gainstracker.nl",
                EmailConfirmed = true,
                NormalizedEmail = $"{username.ToUpper()}@GAINSTRACKER.NL",
                PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "cheeseGrator1!"),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            
            defaultUsers.Add(newUser);
        }
        
        foreach (User u in defaultUsers)
        {
            u.GainsAccount = null;
            string display = u.UserName == "stije" ? "DavrozzGaining" : u.UserName == "joyo" ? "DinosaurEnjoyer" : "";
            var gains = new GainsAccount(u.UserName!, display)
            {
                UserId = u.Id,
                Workouts = new List<Workout>()
            };
            u.GainsAccountId = gains.Id;
            
            gains.UserProfile = null!;
            var profile = new UserProfile(gains.Id);
            gains.UserProfileId = profile.Id;

            profile.Icon = null!;
            ProfileIcon icon = new(profile.Id);
            
            _builder.Entity<GainsAccount>(gainsTable => gainsTable.HasData(gains));
            _builder.Entity<User>(userTable => userTable.HasData(u));
            _builder.Entity<UserProfile>(userTable => userTable.HasData(profile));
            _builder.Entity<ProfileIcon>(userTable => userTable.HasData(icon));
        }
    }

    public static string CreateId()
    {
        return Guid.NewGuid().ToString();
    }
}

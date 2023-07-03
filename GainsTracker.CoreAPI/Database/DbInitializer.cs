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

    public void Seed()
    {
        Console.WriteLine("seeding db..");
        const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
        const string ROLE_ID = ADMIN_ID;

        _builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = ROLE_ID,
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

        User joyo = new("Joyo")
        {
            Id = CreateId(),
            NormalizedUserName = "JOYO",
            Email = "joy@studiostoy.nl",
            EmailConfirmed = false,
            NormalizedEmail = "JOY@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "user"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        User damian = new("BINO")
        {
            Id = CreateId(),
            NormalizedUserName = "BINO",
            Email = "test@studiostoy.nl",
            EmailConfirmed = false,
            NormalizedEmail = "TEST@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "user"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        User soep = new("soep")
        {
            Id = CreateId(),
            NormalizedUserName = "SOEP",
            Email = "test@studiostoy.nl",
            EmailConfirmed = false,
            NormalizedEmail = "TEST@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "user"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        User eef = new("eef")
        {
            Id = CreateId(),
            NormalizedUserName = "EEF",
            Email = "test@studiostoy.nl",
            EmailConfirmed = false,
            NormalizedEmail = "TEST@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "user"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        User jordt = new("jordt")
        {
            Id = CreateId(),
            NormalizedUserName = "JORDT",
            Email = "test@studiostoy.nl",
            EmailConfirmed = false,
            NormalizedEmail = "TEST@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "user"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        User sanda = new("sanda")
        {
            Id = CreateId(),
            NormalizedUserName = "SANDA",
            Email = "test@studiostoy.nl",
            EmailConfirmed = false,
            NormalizedEmail = "TEST@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "user"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        User naoh = new("naoh")
        {
            Id = CreateId(),
            NormalizedUserName = "NAOH",
            Email = "test@studiostoy.nl",
            EmailConfirmed = false,
            NormalizedEmail = "TEST@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "user"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        List<User> defaultUsers = new()
        {
            stije, joyo, damian, soep, eef, jordt, sanda, naoh
        };

        foreach (User u in defaultUsers)
        {
            u.GainsAccount = null;
            var gains = new GainsAccount(u.UserName!)
            {
                UserId = u.Id,
                DisplayName = u.UserName == "stije" ? "DavrozzGaining" : u.UserName == "joyo" ? "DinosaurEnjoyer" : "",
                Workouts = new List<Workout>()
            };
            u.GainsAccountId = gains.Id;
            
            gains.UserProfile = null;
            var profile = new UserProfile(gains.Id);
            gains.UserProfileId = profile.Id;
            
            _builder.Entity<GainsAccount>(gainsTable => gainsTable.HasData(gains));
            _builder.Entity<User>(userTable => userTable.HasData(u));
            _builder.Entity<UserProfile>(userTable => userTable.HasData(profile));
        }
    }

    public static string CreateId()
    {
        return Guid.NewGuid().ToString();
    }
}

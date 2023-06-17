using DotNetEnv;
using GainsTracker.CoreAPI.Components.Security.Models;
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
        const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
        const string ROLE_ID = ADMIN_ID;

        _builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = ROLE_ID,
            Name = "admin",
            NormalizedName = "ADMIN"
        });

        User user = new();
        PasswordHasher<User> hasher = new();

        // When creating a default user, it is necessary to fill in the normalized fields as well as the security stam
        User stije = new()
        {
            Id = CreateId(),
            UserName = "stije",
            NormalizedUserName = "STIJE",
            Email = "stije@studiostoy.nl",
            EmailConfirmed = true,
            NormalizedEmail = "STIJE@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "admin"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        User joyo = new()
        {
            Id = CreateId(),
            UserName = "Joyo",
            NormalizedUserName = "JOYO",
            Email = "joy@studiostoy.nl",
            EmailConfirmed = false,
            NormalizedEmail = "JOY@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "user"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        User damian = new()
        {
            Id = CreateId(),
            UserName = "BINO",
            NormalizedUserName = "BINO",
            Email = "test@studiostoy.nl",
            EmailConfirmed = false,
            NormalizedEmail = "TEST@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "user"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        User soep = new()
        {
            Id = CreateId(),
            UserName = "soep",
            NormalizedUserName = "SOEP",
            Email = "test@studiostoy.nl",
            EmailConfirmed = false,
            NormalizedEmail = "TEST@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "user"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        User eef = new()
        {
            Id = CreateId(),
            UserName = "eef",
            NormalizedUserName = "EEF",
            Email = "test@studiostoy.nl",
            EmailConfirmed = false,
            NormalizedEmail = "TEST@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "user"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        User jordt = new()
        {
            Id = CreateId(),
            UserName = "jordt",
            NormalizedUserName = "JORDT",
            Email = "test@studiostoy.nl",
            EmailConfirmed = false,
            NormalizedEmail = "TEST@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "user"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        User sanda = new()
        {
            Id = CreateId(),
            UserName = "sanda",
            NormalizedUserName = "SANDA",
            Email = "test@studiostoy.nl",
            EmailConfirmed = false,
            NormalizedEmail = "TEST@STUDIOSTOY.NL",
            PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "user"),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        User naoh = new()
        {
            Id = CreateId(),
            UserName = "naoh",
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
            _builder.Entity<User>(userTable => userTable.HasData(u));
            _builder.Entity<GainsAccount>(gainsTable => gainsTable.HasData(
                new GainsAccount
                {
                    Id = CreateId(),
                    UserId = u.Id,
                    DisplayName = u.UserName == "stije" ? "DavrozzGaining" : u.UserName == "joyo" ? "DinosaurEnjoyer" : "",
                    UserHandle = u.UserName,
                    Workouts = new List<Workout>()
                }
            ));
        }
    }

    public static string CreateId()
    {
        return Guid.NewGuid().ToString();
    }
}

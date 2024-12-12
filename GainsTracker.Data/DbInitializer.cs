using System;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.Shared;

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
        Console.WriteLine("If applicable, seeding db..");

        // _builder.Entity<IdentityRole>().HasData(new IdentityRole
        // {
        //     Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
        //     Name = "admin",
        //     NormalizedName = "ADMIN"
        // });
        //
        // User user = new("");
        // PasswordHasher<User> hasher = new();
        //
        // // When creating a default user, it is necessary to fill in the normalized fields as well as the security stam
        // User stije = new("stije")
        // {
        //     Id = CreateId(),
        //     NormalizedUserName = "STIJE",
        //     Email = "stije@studiostoy.nl",
        //     EmailConfirmed = true,
        //     NormalizedEmail = "STIJE@STUDIOSTOY.NL",
        //     PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "adminn"),
        //     SecurityStamp = CreateId()
        // };
        //
        // List<User> defaultUsers = new() { stije };
        // foreach (var username in new[] { "joyo","bino","soep","eef","jordt","sanda","naoh","dyllo","arv","japser"})
        // {
        //     User newUser = new(username)
        //     {
        //         Id = CreateId(),
        //         NormalizedUserName = username.ToUpper(),
        //         Email = $"{username}@gainstracker.nl",
        //         EmailConfirmed = true,
        //         NormalizedEmail = $"{username.ToUpper()}@GAINSTRACKER.NL",
        //         PasswordHash = hasher.HashPassword(user, Env.GetString("EPIC_PASS") ?? "cheeseGrator1!"),
        //         SecurityStamp = CreateId()
        //     };
        //     
        //     defaultUsers.Add(newUser);
        // }
        //
        // foreach (User u in defaultUsers)
        // {
        //     u.GainsAccountEntity = null;
        //     string display = u.UserName == "stije" ? "DavrozzGaining" : u.UserName == "joyo" ? "DinosaurEnjoyer" : "";
        //     var gains = new GainsAccountEntity(u.UserName!, display)
        //     {
        //         UserId = u.Id,
        //         Workouts = new List<WorkoutEntity>()
        //     };
        //     u.GainsAccountId = gains.Id;
        //     
        //     gains.UserProfileEntity = null!;
        //     var profile = new UserProfileEntity(gains.Id);
        //     gains.UserProfileId = profile.Id;
        //
        //     profile.Icon = null!;
        //     ProfileIconEntity icon = new(profile.Id);
        //     
        //     _builder.Entity<GainsAccountEntity>(gainsTable => gainsTable.HasData(gains));
        //     _builder.Entity<User>(userTable => userTable.HasData(u));
        //     _builder.Entity<UserProfileEntity>(userTable => userTable.HasData(profile));
        //     _builder.Entity<ProfileIconEntity>(userTable => userTable.HasData(icon));
        // }
    }

    public static string CreateId()
    {
        return Guid.NewGuid().ToString();
    }
}

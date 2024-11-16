using GainsTracker.Data.Gains.Entities;
using GainsTracker.Data.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Infrastructure.Data;

public static class DataServiceCollections
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, string connectionString)
    {
        // Register DbContext with the configured connection string
        services.AddDbContext<GainsDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Set up Identity with the GainsDbContext and configure options
        services.AddIdentity<UserEntity, IdentityRole>()
            .AddEntityFrameworkStores<GainsDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static void ResetDatabase(this IServiceScope scope)
    {
        using var db = scope.ServiceProvider.GetRequiredService<GainsDbContext>();

        Console.WriteLine("Resetting database..");

        db.Database.EnsureDeleted();
        db.Database.Migrate();
    }

    public static void ApplyMigrationsToDatabase(this IServiceScope scope)
    {
        using var db = scope.ServiceProvider.GetRequiredService<GainsDbContext>();

        Console.WriteLine("Applying possible migrations..");

        db.Database.Migrate();
    }
}

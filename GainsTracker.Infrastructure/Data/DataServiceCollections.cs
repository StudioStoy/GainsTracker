#region

using GainsTracker.Core.Auth.Models;
using GainsTracker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#endregion

namespace GainsTracker.Infrastructure.Data;

public static class DataServiceCollections
{
    public static IServiceCollection AddDataServices(
        this IServiceCollection services,
        string connectionString,
        bool useInMemory)
    {
        // Set up an in-memory or an actually implemented database.
        if (useInMemory)
            services.AddDbContext<GainsDbContext>(options =>
                options
                    .UseInMemoryDatabase("InMemoryDb")
                    .LogTo(Console.WriteLine, LogLevel.Information)
            );
        else
            services.AddDbContext<GainsDbContext>(options => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
                .LogTo(Console.WriteLine, LogLevel.Information)
            );

        // Set up Identity with the GainsDbContext and configure options
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<GainsDbContext>()
            .AddDefaultTokenProviders();

        services.AddSingleton<GainsDbContextFactory>();

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

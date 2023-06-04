using GainsTracker.CoreAPI.Configurations.Database;
using GainsTracker.CoreAPI.Configurations.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.CoreAPI.Configurations;

public static class ProgramAppExtensions
{
    /// <summary>
    ///     Useful for debugging.
    /// </summary>
    /// <param name="execute">Flag to execute this function or not.</param>
    public static void ResetAndUpdateDatabase(this WebApplication app, bool execute = true)
    {
        if (!execute)
            return;

        using IServiceScope scope = app.Services.CreateScope();
        AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        Console.WriteLine("Resetting database..");
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        db.Database.Migrate();
    }

    /// <summary>
    ///     Configure global exception handling.
    /// </summary>
    public static void AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}

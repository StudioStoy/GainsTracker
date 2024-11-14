using GainsTracker.Data.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var connectionString = environment switch
        {
            "Development" => configuration.GetConnectionString("Development"),
            "Docker" => configuration.GetConnectionString("Docker"),
            "Staging" => configuration.GetConnectionString("Staging"),
            "Production" => configuration.GetConnectionString("Production"),
            _ => configuration.GetConnectionString("Development"),
        };

        services.AddDbContext<GainsDbContext>(options =>
            options.UseNpgsql(connectionString)); // Use your preferred EF provider here

        return services;
    }
}

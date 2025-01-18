using GainsTracker.Core.Users.Interfaces;
using GainsTracker.Core.Users.Interfaces.Repositories;
using GainsTracker.Core.Users.Services;
using GainsTracker.Data.Users;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Infrastructure;

public static class UserServiceCollections
{
    public static IServiceCollection AddUserServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}

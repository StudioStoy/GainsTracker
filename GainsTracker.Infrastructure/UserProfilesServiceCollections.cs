using GainsTracker.Core.UserProfiles.Interfaces.Repositories;
using GainsTracker.Core.UserProfiles.Interfaces.Services;
using GainsTracker.Core.UserProfiles.Services;
using GainsTracker.Data.UserProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Infrastructure;

public static class UserProfilesServiceCollections
{
    public static IServiceCollection AddUserProfileServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();

        return services;
    }
}

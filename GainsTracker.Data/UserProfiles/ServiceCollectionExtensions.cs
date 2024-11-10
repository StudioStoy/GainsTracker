using GainsTracker.Core.UserProfiles.Interfaces.Repositories;
using GainsTracker.Core.UserProfiles.Interfaces.Services;
using GainsTracker.Core.UserProfiles.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Data.UserProfiles;

public static class ServerCollectionExtensions
{
    public static IServiceCollection AddUserProfileServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IUserProfileBigBrain, UserProfileBigBrain>();
        
        return services;
    }
}

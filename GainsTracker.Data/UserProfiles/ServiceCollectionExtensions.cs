using GainsTracker.Core.Components.UserProfiles.Interfaces.Repositories;
using GainsTracker.Core.Components.UserProfiles.Interfaces.Services;
using GainsTracker.Core.Components.UserProfiles.Services;
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

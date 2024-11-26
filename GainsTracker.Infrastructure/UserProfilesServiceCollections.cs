using GainsTracker.Core;
using GainsTracker.Core.UserProfiles.Interfaces.Repositories;
using GainsTracker.Core.UserProfiles.Interfaces.Services;
using GainsTracker.Core.UserProfiles.Models;
using GainsTracker.Core.UserProfiles.Services;
using GainsTracker.Data;
using GainsTracker.Data.UserProfiles;
using GainsTracker.Data.UserProfiles.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Infrastructure;

public static class UserProfilesServiceCollections
{
    public static IServiceCollection AddUserProfileServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IUserProfileBigBrain, UserProfileRepository>();
        services.AddTransient(typeof(IGenericRepository<UserProfile>),
            typeof(GenericRepository<UserProfile, UserProfileEntity>));

        return services;
    }
}

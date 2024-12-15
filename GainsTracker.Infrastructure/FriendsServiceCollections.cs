using GainsTracker.Core.Friends.Interfaces.Repositories;
using GainsTracker.Core.Friends.Interfaces.Services;
using GainsTracker.Core.Friends.Services;
using GainsTracker.Data.Friends;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Infrastructure;

public static class FriendsServiceCollections
{
    public static IServiceCollection AddFriendServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IFriendService, FriendService>();
        services.AddScoped<IFriendRepository, FriendRepository>();
        services.AddScoped<IFriendRequestService, FriendRequestService>();

        return services;
    }
}

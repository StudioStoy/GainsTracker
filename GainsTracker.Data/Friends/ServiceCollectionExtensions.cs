using GainsTracker.Core.Components.Friends.Interfaces.Repositories;
using GainsTracker.Core.Components.Friends.Interfaces.Services;
using GainsTracker.Core.Components.Friends.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GainsTracker.Data.Friends;

public static class ServerCollectionExtensions
{
    public static IServiceCollection AddFriendServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IFriendService, FriendService>();
        services.AddScoped<IFriendBigBrain, FriendBigBrain>();
        services.AddScoped<IFriendRequestService, FriendRequestService>();
        
        return services;
    }
}

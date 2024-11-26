using GainsTracker.Core;
using GainsTracker.Core.Friends.Interfaces.Repositories;
using GainsTracker.Core.Friends.Interfaces.Services;
using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.Friends.Services;
using GainsTracker.Data;
using GainsTracker.Data.Friends;
using GainsTracker.Data.Friends.Entities;
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
        services.AddTransient(typeof(IGenericRepository<Friend>), typeof(GenericRepository<Friend, FriendEntity>));

        return services;
    }
}

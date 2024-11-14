using GainsTracker.Common.Models.Friends.Dto;
using GainsTracker.Core.Friends.Exceptions;
using GainsTracker.Core.Friends.Interfaces.Repositories;
using GainsTracker.Core.Friends.Interfaces.Services;
using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Gains.Models;
using GainsTracker.Core.Workouts.Models;

namespace GainsTracker.Core.Friends.Services;

public class FriendRequestService(IFriendBigBrain bigBrain, IGainsService gainsService) : IFriendRequestService
{
    public async Task<FriendRequestOverviewDto> GetFriendRequests(string userHandle)
    {
        Guid gainsId = await gainsService.GetGainsIdByUsername(userHandle);
        GainsAccount user = await bigBrain.GetFriendInfoByGainsId(gainsId);

        return new FriendRequestOverviewDto
        {
            Sent = user.SentFriendRequests.Select(f => f.ToDto()).ToList(),
            Received = user.ReceivedFriendRequests.Select(f => f.ToDto()).ToList()
        };
    }

    public async Task SendFriendRequest(string userHandle, string friendHandle)
    {
        await CheckFriendshipStatus(userHandle, friendHandle);

        GainsAccount user = await gainsService.GetGainsAccountByUserHandle(userHandle);
        GainsAccount potentialFriend = await gainsService.GetGainsAccountByUserHandle(friendHandle);

        user.SentFriendRequest(potentialFriend);
        
        await bigBrain.SaveContext();
    }

    public async Task HandleFriendRequestState(string userHandle, Guid requestId, bool accept = true)
    {
        FriendRequest request = await bigBrain.GetFriendRequestById(requestId);
        Guid gainsId = await gainsService.GetGainsIdByUsername(userHandle);

        if (request.RequesterId == gainsId)
            throw new Exception("Requester can obviously not accept or reject their own request");

        if (accept) request.Accept();
        else request.Reject();

        await bigBrain.SaveContext();
    }

    private async Task<List<Friend>> GetFriends(string userHandle)
    {
        GainsAccount gainsAccount = await gainsService.GetGainsAccountByUserHandle(userHandle);
        List<Friend> friends = await bigBrain.GetFriendsByGainsId(gainsAccount.Id);

        return friends;
    }

    private async Task CheckFriendshipStatus(string userHandle, string friendHandle)
    {
        if (await AreFriends(userHandle, friendHandle))
            throw new AlreadyFriendsException($"You are already friends with {friendHandle}!");

        if (await FriendRequestAlreadySent(userHandle, friendHandle))
            throw new FriendRequestAlreadySentException($"You already sent a friend request to {friendHandle}!");
    }

    private async Task<bool> AreFriends(string userHandle, string friendHandle)
    {
        List<Friend> userFriends = await GetFriends(userHandle);
        return userFriends.Any(friend => string.Equals(friend.FriendHandle, friendHandle, StringComparison.InvariantCultureIgnoreCase));
    }

    private async Task<bool> FriendRequestAlreadySent(string userHandle, string friendHandle)
    {
        FriendRequestOverviewDto friendRequest = await GetFriendRequests(userHandle);
        return friendRequest.Sent.Any(req =>
            string.Equals(req.RecipientName, friendHandle, StringComparison.InvariantCultureIgnoreCase));
    }
}

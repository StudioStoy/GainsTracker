using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.Friends.Dto;
using GainsTracker.Core.Friends.Exceptions;
using GainsTracker.Core.Friends.Interfaces.Repositories;
using GainsTracker.Core.Friends.Interfaces.Services;
using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.Gains.Interfaces.Services;

namespace GainsTracker.Core.Friends.Services;

public class FriendRequestService(IFriendRepository repository, IGainsService gainsService) : IFriendRequestService
{
    public async Task<FriendRequestOverviewDto> GetFriendRequests(string userHandle)
    {
        var gainsId = await gainsService.GetGainsIdByUsername(userHandle);
        var user = await repository.GetFriendInfoByGainsId(gainsId);

        return new FriendRequestOverviewDto
        {
            Sent = user.SentFriendRequests.Select(f => f.ToDto()).ToList(),
            Received = user.ReceivedFriendRequests.Select(f => f.ToDto()).ToList(),
        };
    }

    public async Task SendFriendRequest(string userHandle, string friendHandle)
    {
        await CheckFriendshipStatus(userHandle, friendHandle);

        var user = await gainsService.GetGainsAccountByUserHandle(userHandle);
        var potentialFriend = await gainsService.GetGainsAccountByUserHandle(friendHandle);

        var friendRequest = user.SentFriendRequest(potentialFriend);
        await AddFriendRequestWithoutAccounts(friendRequest);

        // await gainsService.UpdateGainsAccount(user);
        // await gainsService.UpdateGainsAccount(potentialFriend);
    }

    public async Task HandleFriendRequestState(string userHandle, Guid requestId, bool accept = true)
    {
        var request = await repository.GetFriendRequestById(requestId);
        var gainsId = await gainsService.GetGainsIdByUsername(userHandle);

        if (request.RequesterId == gainsId)
            throw new ForbiddenException("Requester can obviously not accept or reject their own request");

        if (accept) request.Accept();
        else request.Reject();

        await repository.UpdateFriendRequest(request);
    }

    private async Task<List<Friend>> GetFriends(string userHandle)
    {
        var gainsAccount = await gainsService.GetGainsAccountByUserHandle(userHandle);
        var friends = await repository.GetFriendsByGainsId(gainsAccount.Id);

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
        var userFriends = await GetFriends(userHandle);
        return userFriends.Any(friend =>
            string.Equals(friend.FriendHandle, friendHandle, StringComparison.InvariantCultureIgnoreCase));
    }

    private async Task<bool> FriendRequestAlreadySent(string userHandle, string friendHandle)
    {
        var friendRequest = await GetFriendRequests(userHandle);
        return friendRequest.Sent.Any(req =>
            string.Equals(req.RecipientName, friendHandle, StringComparison.InvariantCultureIgnoreCase));
    }

    private async Task AddFriendRequestWithoutAccounts(FriendRequest friendRequest)
    {
        friendRequest.Requester = null!;
        friendRequest.Recipient = null!;
        await repository.AddFriendRequest(friendRequest);
    }
}

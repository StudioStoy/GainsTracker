using GainsTrackerAPI.Gains.Data;
using GainsTrackerAPI.Gains.Models;
using GainsTrackerAPI.Gains.Models.Exceptions;
using GainsTrackerAPI.Gains.Models.Friends;
using GainsTrackerAPI.Gains.Services.Dto;

namespace GainsTrackerAPI.Gains.Services;

public class FriendService : IFriendService
{
    private readonly BigBrain _bigBrain;
    private readonly IGainsService _gainsService;

    public FriendService(BigBrain bigBrain, IGainsService gainsService)
    {
        _bigBrain = bigBrain;
        _gainsService = gainsService;
    }

    public async Task<List<Friend>> GetFriends(string username)
    {
        GainsAccount gainsAccount = await _gainsService.GetGainsAccountFromUser(username);
        List<Friend> friends = _bigBrain.GetFriendsByGainsId(gainsAccount.Id);

        return friends;
    }

    public async Task<FriendRequestOverviewDto> GetFriendRequests(string username)
    {
        string accountId = (await _gainsService.GetGainsAccountFromUser(username)).Id;
        GainsAccount user = _bigBrain.GetFriendInfoByGainsId(accountId);

        return new FriendRequestOverviewDto
        {
            Sent = user.SentFriendRequests.Select(FriendRequestDto.FromFriendRequest).ToList(),
            Received = user.ReceivedFriendRequests.Select(FriendRequestDto.FromFriendRequest).ToList()
        };
    }

    public async Task SendFriendRequest(string username, string friendName)
    {
        GainsAccount user = await _gainsService.GetGainsAccountFromUser(username);
        GainsAccount potentialFriend = await _gainsService.GetGainsAccountFromUser(friendName);

        user.SentFriendRequest(potentialFriend);
        _bigBrain.SaveContext();
    }

    private async Task CheckFriendshipStatus(string username, string friendName)
    {
        if (await AreFriends(username, friendName))
            throw new AlreadyFriendsException($"You are already friends with {friendName}!");

        if (await FriendRequestAlreadySent(username, friendName))
            throw new FriendRequestAlreadySentException($"You already sent a friend request to {friendName}!");
    }

    private async Task<bool> AreFriends(string username, string friendName)
    {
        List<Friend> userFriends = await GetFriends(username);
        return userFriends.Any(friend =>
            string.Equals(friend.Name, friendName, StringComparison.InvariantCultureIgnoreCase));
    }

    private async Task<bool> FriendRequestAlreadySent(string username, string friendName)
    {
        FriendRequestOverviewDto egg = await GetFriendRequests(username);
        return egg.Sent.Any(req =>
            string.Equals(req.RequestedToName, friendName, StringComparison.InvariantCultureIgnoreCase));
    }
}

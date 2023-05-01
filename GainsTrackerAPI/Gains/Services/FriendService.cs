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

    public List<Friend> GetFriends(string username)
    {
        GainsAccount gainsAccount = _gainsService.GetGainsAccountFromUser(username);
        List<Friend> friends = _bigBrain.GetFriendsByGainsId(gainsAccount.Id);

        return friends;
    }

    public FriendRequestOverviewDto GetFriendRequests(string username)
    {
        string accountId = (_gainsService.GetGainsAccountFromUser(username)).Id;
        GainsAccount user = _bigBrain.GetFriendInfoByGainsId(accountId);

        return new FriendRequestOverviewDto
        {
            Sent = user.SentFriendRequests.Select(FriendRequestDto.FromFriendRequest).ToList(),
            Received = user.ReceivedFriendRequests.Select(FriendRequestDto.FromFriendRequest).ToList()
        };
    }

    public void SendFriendRequest(string username, string friendName)
    {
        CheckFriendshipStatus(username, friendName);
        
        GainsAccount user = _gainsService.GetGainsAccountFromUser(username);
        GainsAccount potentialFriend = _gainsService.GetGainsAccountFromUser(friendName);

        user.SentFriendRequest(potentialFriend);
        _bigBrain.SaveContext();
    }

    public void HandleFriendRequestState(string requestId, bool accept=true)
    {
        var request = _bigBrain.GetFriendRequestById(requestId);

        if (accept) request.Accept();
        else request.Reject();
        
        _bigBrain.SaveContext();
    }

    private void CheckFriendshipStatus(string username, string friendName)
    {
        if (AreFriends(username, friendName))
            throw new AlreadyFriendsException($"You are already friends with {friendName}!");

        if (FriendRequestAlreadySent(username, friendName))
            throw new FriendRequestAlreadySentException($"You already sent a friend request to {friendName}!");
    }

    private bool AreFriends(string username, string friendName)
    {
        List<Friend> userFriends = GetFriends(username);
        return userFriends.Any(friend =>
            string.Equals(friend.Name, friendName, StringComparison.InvariantCultureIgnoreCase));
    }

    private bool FriendRequestAlreadySent(string username, string friendName)
    {
        FriendRequestOverviewDto egg = GetFriendRequests(username);
        return egg.Sent.Any(req =>
            string.Equals(req.RequestedToName, friendName, StringComparison.InvariantCultureIgnoreCase));
    }
}

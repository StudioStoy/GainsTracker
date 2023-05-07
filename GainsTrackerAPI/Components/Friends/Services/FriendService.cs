using GainsTrackerAPI.Components.Friends.Data;
using GainsTrackerAPI.Components.Friends.Models;
using GainsTrackerAPI.Components.Friends.Models.Exceptions;
using GainsTrackerAPI.Components.Friends.Services.Dto;
using GainsTrackerAPI.Components.Gains.Models;
using GainsTrackerAPI.Components.Gains.Services;

namespace GainsTrackerAPI.Components.Friends.Services;

public class FriendService : IFriendService
{
    private readonly BigBrainFriends _bigBrain;
    private readonly IGainsService _gainsService;

    public FriendService(BigBrainFriends bigBrain, IGainsService gainsService)
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
        string accountId = _gainsService.GetGainsAccountFromUser(username).Id;
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

    public void HandleFriendRequestState(string username, string requestId, bool accept = true)
    {
        FriendRequest request = _bigBrain.GetFriendRequestById(requestId);
        string gainsId = _bigBrain.GetGainsIdByUsername(username);

        if (request.RequestedById == gainsId)
            throw new Exception("Requester can obviously not accept their own request");

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
        FriendRequestOverviewDto friendRequest = GetFriendRequests(username);
        return friendRequest.Sent.Any(req =>
            string.Equals(req.RequestedToName, friendName, StringComparison.InvariantCultureIgnoreCase));
    }
}

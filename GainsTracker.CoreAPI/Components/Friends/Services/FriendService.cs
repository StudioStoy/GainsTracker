using GainsTracker.CoreAPI.Components.Friends.Data;
using GainsTracker.CoreAPI.Components.Friends.Models;
using GainsTracker.CoreAPI.Components.Friends.Models.Exceptions;
using GainsTracker.CoreAPI.Components.Friends.Services.Dto;
using GainsTracker.CoreAPI.Components.Gains.Models;
using GainsTracker.CoreAPI.Components.Gains.Services;

namespace GainsTracker.CoreAPI.Components.Friends.Services;

public class FriendService : IFriendService
{
    private readonly BigBrainFriends _bigBrain;
    private readonly IGainsService _gainsService;

    public FriendService(BigBrainFriends bigBrain, IGainsService gainsService)
    {
        _bigBrain = bigBrain;
        _gainsService = gainsService;
    }

    public List<Friend> GetFriends(string userHandle)
    {
        GainsAccount gainsAccount = _gainsService.GetGainsAccountFromUser(userHandle);
        List<Friend> friends = _bigBrain.GetFriendsByGainsId(gainsAccount.Id);

        return friends;
    }

    public FriendRequestOverviewDto GetFriendRequests(string userHandle)
    {
        string accountId = _gainsService.GetGainsAccountFromUser(userHandle).Id;
        GainsAccount user = _bigBrain.GetFriendInfoByGainsId(accountId);

        return new FriendRequestOverviewDto
        {
            Sent = user.SentFriendRequests.Select(FriendRequestDto.FromFriendRequest).ToList(),
            Received = user.ReceivedFriendRequests.Select(FriendRequestDto.FromFriendRequest).ToList()
        };
    }

    public void SendFriendRequest(string userHandle, string friendHandle)
    {
        CheckFriendshipStatus(userHandle, friendHandle);

        GainsAccount user = _gainsService.GetGainsAccountFromUser(userHandle);
        GainsAccount potentialFriend = _gainsService.GetGainsAccountFromUser(friendHandle);

        user.SentFriendRequest(potentialFriend);
        _bigBrain.SaveContext();
    }

    public void HandleFriendRequestState(string userHandle, string requestId, bool accept = true)
    {
        FriendRequest request = _bigBrain.GetFriendRequestById(requestId);
        string gainsId = _bigBrain.GetGainsIdByUsername(userHandle);

        if (request.RequestedById == gainsId)
            throw new Exception("Requester can obviously not accept or reject their own request");

        if (accept) request.Accept();
        else request.Reject();

        _bigBrain.SaveContext();
    }

    private void CheckFriendshipStatus(string userHandle, string friendHandle)
    {
        if (AreFriends(userHandle, friendHandle))
            throw new AlreadyFriendsException($"You are already friends with {friendHandle}!");

        if (FriendRequestAlreadySent(userHandle, friendHandle))
            throw new FriendRequestAlreadySentException($"You already sent a friend request to {friendHandle}!");
    }

    private bool AreFriends(string userHandle, string friendHandle)
    {
        List<Friend> userFriends = GetFriends(userHandle);
        return userFriends.Any(friend =>
            string.Equals(friend.FriendHandle, friendHandle, StringComparison.InvariantCultureIgnoreCase));
    }

    private bool FriendRequestAlreadySent(string userHandle, string friendHandle)
    {
        FriendRequestOverviewDto friendRequest = GetFriendRequests(userHandle);
        return friendRequest.Sent.Any(req =>
            string.Equals(req.RequestedToName, friendHandle, StringComparison.InvariantCultureIgnoreCase));
    }
}

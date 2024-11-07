using GainsTracker.Common.Models.Friends.Dto;
using GainsTracker.Core.Components.Friends.Data;
using GainsTracker.Core.Components.Friends.Models;
using GainsTracker.Core.Components.Friends.Models.Exceptions;
using GainsTracker.Core.Components.Workouts.Models;
using GainsTracker.Data.Friends;
using Friend = GainsTracker.Core.Components.Friends.Models.Friend;

namespace GainsTracker.Core.Components.Friends.Services;

public class FriendRequestService : IFriendRequestService
{
    private readonly BigBrainFriend _bigBrain;

    public FriendRequestService(BigBrainFriend bigBrain)
    {
        _bigBrain = bigBrain;
    }

    public FriendRequestOverviewDto GetFriendRequests(string userHandle)
    {
        string accountId = _bigBrain.GetGainsAccountByUserHandle(userHandle).Id;
        GainsAccount user = _bigBrain.GetFriendInfoByGainsId(accountId);

        return new FriendRequestOverviewDto
        {
            Sent = user.SentFriendRequests.Select(f => f.ToDto()).ToList(),
            Received = user.ReceivedFriendRequests.Select(f => f.ToDto()).ToList()
        };
    }

    public void SendFriendRequest(string userHandle, string friendHandle)
    {
        CheckFriendshipStatus(userHandle, friendHandle);

        GainsAccount user = _bigBrain.GetGainsAccountByUserHandle(userHandle);
        GainsAccount potentialFriend = _bigBrain.GetGainsAccountByUserHandle(friendHandle);

        user.SentFriendRequest(potentialFriend);
        _bigBrain.SaveContext();
    }

    public void HandleFriendRequestState(string userHandle, string requestId, bool accept = true)
    {
        FriendRequest request = _bigBrain.GetFriendRequestById(requestId);
        string gainsId = _bigBrain.GetGainsIdByUsername(userHandle);

        if (request.RequesterId == gainsId)
            throw new Exception("Requester can obviously not accept or reject their own request");

        if (accept) request.Accept();
        else request.Reject();

        _bigBrain.SaveContext();
    }

    public List<Friend> GetFriends(string userHandle)
    {
        GainsAccount gainsAccount = _bigBrain.GetGainsAccountByUserHandle(userHandle);
        List<Friend> friends = _bigBrain.GetFriendsByGainsId(gainsAccount.Id);

        return friends;
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
            string.Equals(req.RecipientName, friendHandle, StringComparison.InvariantCultureIgnoreCase));
    }
}

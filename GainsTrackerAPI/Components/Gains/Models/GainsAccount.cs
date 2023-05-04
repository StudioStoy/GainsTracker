using GainsTrackerAPI.Components.Friends.Models;
using GainsTrackerAPI.Components.Friends.Models.Exceptions;
using GainsTrackerAPI.Components.Gains.Models.Workouts;

namespace GainsTrackerAPI.Components.Gains.Models;

public class GainsAccount
{
    public string Username { get; set; } = "";
    public List<Workout> Workouts { get; set; } = new();
    public List<Friend> Friends { get; set; } = new();
    public List<FriendRequest> ReceivedFriendRequests { get; set; } = new();
    public List<FriendRequest> SentFriendRequests { get; set; } = new();

    public void SentFriendRequest(GainsAccount toPotentialFriend)
    {
        CheckFriendRequests(toPotentialFriend.Username);

        FriendRequest request = new(this, toPotentialFriend);

        SentFriendRequests.Add(request);
        toPotentialFriend.ReceivedFriendRequests.Add(request);
    }

    // This method is not always reliable, as EF core usually loads entities in without all properties.
    // Because of this they can appear empty even though they are not. So always also check in the service layer.
    private void CheckFriendRequests(string friendName)
    {
        if (SentFriendRequests.Any(req =>
                string.Equals(req.RequestedTo.Username, friendName,
                    StringComparison.InvariantCultureIgnoreCase))
            || ReceivedFriendRequests.Any(req =>
                string.Equals(req.RequestedTo.Username, friendName,
                    StringComparison.InvariantCultureIgnoreCase)))
            throw new FriendRequestAlreadySentException($"You already sent a friend request to {friendName}!");
    }

    #region Relations

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }

    #endregion
}

using GainsTracker.CoreAPI.Components.Friend.Models;
using GainsTracker.CoreAPI.Components.Friend.Models.Exceptions;

namespace GainsTracker.CoreAPI.Components.Workout.Models;

public class GainsAccount
{
    public string UserHandle { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public List<Workouts.Workout> Workouts { get; set; } = new();
    public List<Friend.Models.Friend> Friends { get; set; } = new();
    public List<FriendRequest> ReceivedFriendRequests { get; set; } = new();
    public List<FriendRequest> SentFriendRequests { get; set; } = new();

    public void SentFriendRequest(GainsAccount toPotentialFriend)
    {
        CheckFriendRequests(toPotentialFriend.UserHandle);

        FriendRequest request = new(this, toPotentialFriend);

        SentFriendRequests.Add(request);
        toPotentialFriend.ReceivedFriendRequests.Add(request);
    }

    public void AddWorkout(Workouts.Workout workout)
    {
        Workouts.Add(workout);
    }

    // This method is not always reliable, as EF core usually loads entities in without all properties.
    // Because of this they can appear empty even though they are not. So always also check in the service layer.
    private void CheckFriendRequests(string friendName)
    {
        if (SentFriendRequests.Any(req =>
                string.Equals(req.RequestedTo.UserHandle, friendName,
                    StringComparison.InvariantCultureIgnoreCase))
            || ReceivedFriendRequests.Any(req =>
                string.Equals(req.RequestedTo.UserHandle, friendName,
                    StringComparison.InvariantCultureIgnoreCase)))
            throw new FriendRequestAlreadySentException($"You already sent a friend request to {friendName}!");
    }

    #region Relations

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;

    #endregion
}

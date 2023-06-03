using GainsTracker.CoreAPI.Components.Friends.Models;
using GainsTracker.CoreAPI.Components.Friends.Models.Exceptions;
using GainsTracker.CoreAPI.Components.HealthMetrics.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;

namespace GainsTracker.CoreAPI.Components.Workouts.Models;

public class GainsAccount
{
    public string UserHandle { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public List<Workout> Workouts { get; set; } = new();
    public List<Metric> Metrics { get; set; } = new();
    public List<Friend> Friends { get; set; } = new();
    public List<FriendRequest> ReceivedFriendRequests { get; set; } = new();
    public List<FriendRequest> SentFriendRequests { get; set; } = new();

    public void SentFriendRequest(GainsAccount toPotentialFriend)
    {
        CheckFriendRequests(toPotentialFriend.UserHandle);

        FriendRequest request = new(this, toPotentialFriend);

        SentFriendRequests.Add(request);
        toPotentialFriend.ReceivedFriendRequests.Add(request);
    }

    public void AddWorkout(Workout workout)
    {
        Workouts.Add(workout);
    }

    public void AddMetric(Metric trackableGoal)
    {
        Metrics.Add(trackableGoal);
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

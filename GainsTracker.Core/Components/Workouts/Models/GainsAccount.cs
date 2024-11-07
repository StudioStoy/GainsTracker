using GainsTracker.Core.Components.Friends.Models;
using GainsTracker.Core.Components.Friends.Models.Exceptions;
using GainsTracker.Core.Components.HealthMetrics.Models;
using GainsTracker.Core.Components.UserProfiles.Models;
using GainsTracker.Core.Components.Workouts.Models.Workouts;

namespace GainsTracker.Core.Components.Workouts.Models;

public class GainsAccount
{
    private GainsAccount() {}
    
    public GainsAccount(string userHandle, string displayName = "")
    {
        Id = Guid.NewGuid().ToString();
        UserHandle = userHandle;
        UserProfile = new UserProfile(Id, string.IsNullOrWhiteSpace(displayName) ? userHandle : displayName);
        UserProfileId = UserProfile.Id;
    }

    public string UserHandle { get; set; } = "";

    public UserProfile UserProfile { get; set; } = null!;

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

    private void CheckFriendRequests(string friendName)
    {
        if (SentFriendRequests.Any(req =>
                string.Equals(req.Recipient.UserHandle, friendName,
                    StringComparison.InvariantCultureIgnoreCase))
            || ReceivedFriendRequests.Any(req =>
                string.Equals(req.Recipient.UserHandle, friendName,
                    StringComparison.InvariantCultureIgnoreCase)))
            throw new FriendRequestAlreadySentException($"You already sent a friend request to {friendName}!");
    }

    #region Relations

    public string Id { get; set; } = null!;
    public string UserId { get; set; } = "";
    public string UserProfileId { get; set; } = null!;

    #endregion
}

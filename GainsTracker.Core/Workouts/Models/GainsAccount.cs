using GainsTracker.Core.Components.Friends.Exceptions;
using GainsTracker.Core.Components.Friends.Models;
using GainsTracker.Core.Components.HealthMetrics.Models;
using GainsTracker.Core.Components.UserProfiles.Models;
using GainsTracker.Core.Components.Workouts.Models.Workouts;

namespace GainsTracker.Core.Components.Workouts.Models;

public class GainsAccount
{
    public GainsAccount(string userHandle, string displayName = "")
    {
        Id = Guid.NewGuid();
        UserHandle = userHandle;
        UserProfile = new UserProfile(Id, string.IsNullOrWhiteSpace(displayName) ? userHandle : displayName);
    }
    
    public Guid Id { get; set; }

    public string UserHandle { get; set; } = "";

    public UserProfile UserProfile { get; set; } = null!;

    public List<Workout> Workouts { get; set; } = [];
    public List<HealthMetric> Metrics { get; set; } = [];

    public List<Friend> Friends { get; set; } = [];
    public List<FriendRequest> ReceivedFriendRequests { get; set; } = [];
    public List<FriendRequest> SentFriendRequests { get; set; } = [];

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

    public void AddMetric(HealthMetric trackableGoal)
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
}

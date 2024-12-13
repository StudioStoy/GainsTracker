#region

using GainsTracker.Core.Friends.Exceptions;
using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.HealthMetrics.Models;
using GainsTracker.Core.UserProfiles.Models;
using GainsTracker.Core.Workouts.Models.Workouts;

#endregion

namespace GainsTracker.Core.Gains.Models;

public class GainsAccount
{
    public GainsAccount()
    {
    }

    public GainsAccount(string userHandle, string displayName = "")
    {
        Id = Guid.NewGuid();
        UserHandle = userHandle;
        UserProfile = new UserProfile(Id, string.IsNullOrWhiteSpace(displayName) ? userHandle : displayName);
        UserProfileId = UserProfile.Id;
    }

    public Guid Id { get; set; }

    public string UserHandle { get; set; } = string.Empty;

    public Guid UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }

    public List<Workout> Workouts { get; set; } = [];
    public List<HealthMetric> Metrics { get; set; } = [];

    public List<Friend> Friends { get; set; } = [];
    public List<FriendRequest> ReceivedFriendRequests { get; set; } = [];
    public List<FriendRequest> SentFriendRequests { get; set; } = [];

    public FriendRequest SentFriendRequest(GainsAccount toPotentialFriend)
    {
        CheckFriendRequests(toPotentialFriend.UserHandle);

        FriendRequest request = new(this, toPotentialFriend);

        SentFriendRequests.Add(request);
        toPotentialFriend.ReceivedFriendRequests.Add(request);

        return request;
    }

    public void AddWorkout(Workout workout) => Workouts.Add(workout);

    public void AddMetric(HealthMetric trackableGoal) => Metrics.Add(trackableGoal);

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

using System.ComponentModel.DataAnnotations.Schema;
using GainsTracker.Data.Friends.Entities;
using GainsTracker.Data.HealthMetrics.Entities;
using GainsTracker.Data.UserProfiles.Entities;
using GainsTracker.Data.Workouts.Entities;

namespace GainsTracker.Data.Gains.Entities;

[Table("gains_account")]
public class GainsAccountEntity
{
    internal GainsAccountEntity() {}

    public string UserHandle { get; set; } = "";

    public UserProfileEntity UserProfile { get; set; } = null!;

    public List<WorkoutEntity> Workouts { get; set; } = [];
    public List<HealthMetricEntity> Metrics { get; set; } = [];

    public List<FriendEntity> Friends { get; set; } = [];
    public List<FriendRequestEntity> ReceivedFriendRequests { get; set; } = [];
    public List<FriendRequestEntity> SentFriendRequests { get; set; } = [];
    
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid UserProfileId { get; set; }
}

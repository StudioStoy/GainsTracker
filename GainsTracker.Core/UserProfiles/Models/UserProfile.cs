#region

using GainsTracker.Core.Workouts.Models.Measurements;

#endregion

namespace GainsTracker.Core.UserProfiles.Models;

public class UserProfile
{
    public UserProfile()
    {
    }

    public UserProfile(Guid gainsAccountId, string displayName = "")
    {
        Id = Guid.NewGuid();
        GainsAccountId = gainsAccountId;
        DisplayName = displayName;
        Icon = new ProfileIcon(Id);
    }

    public Guid Id { get; set; }
    public Guid GainsAccountId { get; set; }

    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Measurement> PinnedPBs { get; set; } = [];

    public ProfileIcon Icon { get; set; } = null!;
}

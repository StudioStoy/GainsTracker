using GainsTracker.Core.Workouts.Models.Measurements;

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
        Icon = new ProfileIcon();
    }

    public Guid Id { get; init; }
    public Guid GainsAccountId { get; init; }

    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Measurement> PinnedPBs { get; set; } = [];

    public ProfileIcon Icon { get; init; } = null!;
}

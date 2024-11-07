using GainsTracker.Core.Components.Workouts.Models.Measurements;

namespace GainsTracker.Core.Components.UserProfiles.Models;

public class UserProfile
{
    public string Id { get; set; }
    public string GainsAccountId { get; set; }
    
    public string DisplayName { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<Measurement> PinnedPBs { get; set; } = new();

    public ProfileIcon Icon { get; set; }
    
    public UserProfile(string gainsAccountId, string displayName = "")
    {
        Id = Guid.NewGuid().ToString();
        GainsAccountId = gainsAccountId;

        DisplayName = displayName;
        Icon = new ProfileIcon(Id);
    }
}

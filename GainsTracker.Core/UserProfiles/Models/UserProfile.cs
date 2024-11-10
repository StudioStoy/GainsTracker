using GainsTracker.Core.Workouts.Models.Measurements;

namespace GainsTracker.Core.UserProfiles.Models;

public class UserProfile
{
    public Guid Id { get; set; }
    public Guid GainsAccountId { get; set; }
    
    public string DisplayName { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<Measurement> PinnedPBs { get; set; } = [];

    public ProfileIcon Icon { get; set; }
    
    public UserProfile(Guid gainsAccountId, string displayName = "")
    {
        Id = Guid.NewGuid();
        GainsAccountId = gainsAccountId;

        DisplayName = displayName;
        Icon = new ProfileIcon(Id);
    }
}

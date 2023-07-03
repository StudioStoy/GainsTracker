using GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;

namespace GainsTracker.CoreAPI.Components.UserProfiles.Models;

public class UserProfile
{
    public string Id { get; set; }
    public string GainsAccountId { get; set; }
    
    public string Description { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public List<Measurement> PinnedPBs { get; set; } = new();

    public UserProfile(string gainsAccountId)
    {
        Id = Guid.NewGuid().ToString();
        GainsAccountId = gainsAccountId;
    }
}

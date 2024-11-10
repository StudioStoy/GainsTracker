using System.ComponentModel.DataAnnotations.Schema;
using GainsTracker.Data.Workouts.Entities;

namespace GainsTracker.Data.UserProfiles.Entities;

[Table("user_profile")]
public class UserProfileEntity
{
    public Guid Id { get; set; }
    public Guid GainsAccountId { get; set; }
    
    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<MeasurementEntity> PinnedPBs { get; set; } = [];

    public required ProfileIconEntity Icon { get; set; }
    
}

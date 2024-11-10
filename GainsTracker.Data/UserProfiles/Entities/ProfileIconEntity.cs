using System.ComponentModel.DataAnnotations.Schema;

namespace GainsTracker.Data.UserProfiles.Entities;

[Table("profile_icon")]
public class ProfileIconEntity
{
    public Guid Id { get; set; }
    public Guid UserProfileId { get; set; }
    
    public int PictureColor { get; set; }
    public string Url { get; set; } = string.Empty;
}

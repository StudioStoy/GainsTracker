using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace GainsTracker.Core.Components.UserProfiles.Models;

[Table("profile_icons")]
public class ProfileIcon
{
    public Guid Id { get; set; }
    public Guid UserProfileId { get; set; }
    
    public int PictureColor { get; set; }
    public string Url { get; set; } = string.Empty;

    public ProfileIcon(){}
    
    public ProfileIcon(Guid userProfileId)
    {
        Id = Guid.NewGuid();
        UserProfileId = userProfileId;
        
        Random random = new();
        PictureColor = Color
            .FromArgb(random.Next(256), random.Next(256), random.Next(256))
            .ToArgb();
    }
}

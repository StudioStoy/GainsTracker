using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace GainsTracker.CoreAPI.Components.UserProfiles.Models;

[Table("profile_icons")]
public class ProfileIcon
{
    public string Id { get; set; }
    public string UserProfileId { get; set; }
    
    public int PictureColor { get; set; }
    public string Url { get; set; } = string.Empty;

    public ProfileIcon(){}
    
    public ProfileIcon(string userProfileId)
    {
        Id = Guid.NewGuid().ToString();
        UserProfileId = userProfileId;
        
        Random random = new();
        PictureColor = Color
            .FromArgb(random.Next(256), random.Next(256), random.Next(256))
            .ToArgb();
    }
}

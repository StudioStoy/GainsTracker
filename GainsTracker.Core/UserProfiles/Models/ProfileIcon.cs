using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace GainsTracker.Core.UserProfiles.Models;

[Table("profile_icons")]
public class ProfileIcon
{
    public ProfileIcon()
    {
        Id = Guid.NewGuid();

        Random random = new();
        PictureColor = Color
            .FromArgb(random.Next(256), random.Next(256), random.Next(256))
            .ToArgb();
        Url = string.Empty;
    }

    public Guid Id { get; init; }

    public int PictureColor { get; set; }
    public string Url { get; set; }
}

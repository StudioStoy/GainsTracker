namespace GainsTracker.Common.Models.UserProfiles;

public class UpdatePinnedPBsDto
{
    public List<string> RemovePBs { get; set; } = new();
    public List<string> AddPBs { get; set; } = new();
}

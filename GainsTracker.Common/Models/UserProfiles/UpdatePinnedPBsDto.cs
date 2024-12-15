namespace GainsTracker.Common.Models.UserProfiles;

public record UpdatePinnedPBsDto(List<string> RemovePBs, List<string> AddPBs);

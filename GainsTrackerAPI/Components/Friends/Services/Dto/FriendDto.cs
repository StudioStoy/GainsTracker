using GainsTrackerAPI.Components.Gains.Models;

namespace GainsTrackerAPI.Components.Friends.Services.Dto;

public class FriendDto
{
    public string DisplayName { get; set; } = "";

    public static FriendDto FromGainsAccount(GainsAccount account)
    {
        return new FriendDto
        {
            DisplayName = account.DisplayName
        };
    }
}

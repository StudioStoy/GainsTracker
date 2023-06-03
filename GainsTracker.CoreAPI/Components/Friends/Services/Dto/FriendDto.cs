using GainsTracker.CoreAPI.Components.Workouts.Models;

namespace GainsTracker.CoreAPI.Components.Friends.Services.Dto;

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

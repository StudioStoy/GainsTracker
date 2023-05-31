using GainsTracker.CoreAPI.Components.Workout.Models;

namespace GainsTracker.CoreAPI.Components.Friend.Services.Dto;

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

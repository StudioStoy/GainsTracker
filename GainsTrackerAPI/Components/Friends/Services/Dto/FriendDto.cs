using GainsTrackerAPI.Components.Gains.Models;

namespace GainsTrackerAPI.Components.Friends.Services.Dto;

public class FriendDto
{
    public string Username { get; set; } = "";

    public static FriendDto FromGainsAccount(GainsAccount account)
    {
        return new FriendDto
        {
            Username = account.Username
        };
    }
}

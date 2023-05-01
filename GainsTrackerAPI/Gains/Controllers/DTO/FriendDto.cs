﻿using GainsTrackerAPI.Gains.Models;

namespace GainsTrackerAPI.Gains.Controllers.DTO;

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

﻿namespace GainsTrackerAPI.Gains.Services.Dto;

public class FriendRequestOverviewDto
{
    public List<FriendRequestDto> Sent { get; set; } = new();
    public List<FriendRequestDto> Received { get; set; } = new();
}

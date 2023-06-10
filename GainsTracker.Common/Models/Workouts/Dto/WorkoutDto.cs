﻿namespace GainsTracker.Common.Models.Workouts.Dto;

public class WorkoutDto
{
    public WorkoutDto(string gainsAccountId)
    {
        GainsAccountId = gainsAccountId;
    }

    public string Id { get; set; } = "";
    public string GainsAccountId { get; set; }
    public WorkoutType WorkoutType { get; set; }
    public MeasurementDto? PersonalBest { get; set; }
}
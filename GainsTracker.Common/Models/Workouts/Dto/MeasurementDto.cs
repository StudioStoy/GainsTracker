﻿using System.Text.Json;
using GainsTracker.Common.Models.Measurements.Units;

namespace GainsTracker.Common.Models.Workouts.Dto;

public class MeasurementDto
{
    public string Id { get; set; } = string.Empty;
    public string WorkoutId { get; set; } = string.Empty;
    public ExerciseCategory Category { get; set; }
    public DateTime TimeOfRecord { get; set; }
    public string Notes { get; set; } = string.Empty;
    public JsonDocument? Data { get; set; }
}

public class StrengthMeasurementDto
{
    public WeightUnits WeightUnit { get; set; }
    public double Weight { get; set; }
    public int Reps { get; set; }
}

public class TimeAndDistanceEnduranceMeasurementDto
{
    public string Time { get; set; } = "00:00:00";
    public DistanceUnits DistanceUnit { get; set; }
    public double Distance { get; set; }
}

public class TimeEnduranceMeasurementDto
{
    public double Time { get; set; }
}

public class RepsMeasurementDto
{
    public int Reps { get; set; }
}

public class GeneralMeasurementDto
{
    public string General { get; set; } = string.Empty;
}

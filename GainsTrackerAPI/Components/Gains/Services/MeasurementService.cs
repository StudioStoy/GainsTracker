﻿using GainsTrackerAPI.Components.Gains.Models.Measurements;
using GainsTrackerAPI.Components.Gains.Models.Workouts;
using GainsTrackerCommon.Models.Exceptions;

namespace GainsTrackerAPI.Components.Gains.Services;

/// <summary>
///     This class contains functions for managing all the types of measurements.
/// </summary>
public class MeasurementService : IMeasurementService
{
    public void ValidateMeasurement(Measurement measurement)
    {
        switch (measurement.Category)
        {
            case ExerciseCategory.Strength:
                StrengthMeasurement? strength = measurement as StrengthMeasurement;
                if (strength!.Weight <= 0 || strength.TotalReps <= 0)
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            case ExerciseCategory.SimpleRep:
                SimpleRepMeasurement? simpleRep = measurement as SimpleRepMeasurement;
                if (simpleRep!.Reps <= 0)
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            case ExerciseCategory.SimpleEndurance:
                SimpleEnduranceMeasurement? simpleEndurance = measurement as SimpleEnduranceMeasurement;
                if (simpleEndurance!.Time <= 0)
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            case ExerciseCategory.RunningEndurance:
                RunningEnduranceMeasurement? running = measurement as RunningEnduranceMeasurement;
                if (running!.Time <= 0 || running.Distance <= 0)
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            default:
                throw new ArgumentOutOfRangeException("egg");
        }
    }
}
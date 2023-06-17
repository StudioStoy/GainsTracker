﻿using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;

namespace GainsTracker.CoreAPI.Components.Workouts.Services;

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
                if (strength!.Weight <= 0 || strength.Reps <= 0)
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            case ExerciseCategory.Reps:
                RepsMeasurement? simpleRep = measurement as RepsMeasurement;
                if (simpleRep!.Reps <= 0)
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            case ExerciseCategory.TimeEndurance:
                TimeEnduranceMeasurement? simpleEndurance = measurement as TimeEnduranceMeasurement;
                if (simpleEndurance!.Time != "00:00:00")
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            case ExerciseCategory.TimeAndDistanceEndurance:
                TimeAndDistanceEnduranceMeasurement? running = measurement as TimeAndDistanceEnduranceMeasurement;
                if (running!.Time != "00:00:00" || running.Distance <= 0)
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            default:
                throw new ArgumentOutOfRangeException("egg");
        }
    }
}

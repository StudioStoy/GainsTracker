using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Core.Components.Workouts.Models.Measurements;

namespace GainsTracker.Core.Components.Workouts.Services;

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
                if (simpleEndurance!.Time == 0)
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            case ExerciseCategory.TimeAndDistanceEndurance:
                TimeAndDistanceEnduranceMeasurement? running = measurement as TimeAndDistanceEnduranceMeasurement;
                if (running!.Time == 0 || running.Distance <= 0)
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            case ExerciseCategory.General:
                GeneralMeasurement? general = measurement as GeneralMeasurement;
                // TODO: Add validation for every edge type. Like with bouldering,
                // TODO: max three letters (5a+), no higher than 9c, etc.
                if (string.IsNullOrEmpty(general?.GeneralAchievement))
                    throw new BadRequestException("Please provide a valid value.");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ExerciseCategory), "This category is unknown.");
        }
    }
}

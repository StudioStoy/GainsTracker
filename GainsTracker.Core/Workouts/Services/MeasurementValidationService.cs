using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Core.Workouts.Interfaces.Services;
using GainsTracker.Core.Workouts.Models.Measurements;

namespace GainsTracker.Core.Workouts.Services;

/// <summary>
///     This class contains functions for managing all the types of measurements.
/// </summary>
public class MeasurementValidationService : IMeasurementValidationService
{
    public void ValidateMeasurement(Measurement measurement)
    {
        switch (measurement.Category)
        {
            case ExerciseCategory.Strength:
                var strength = measurement as StrengthMeasurement;
                if (strength!.Weight <= 0 || strength.Reps <= 0)
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            case ExerciseCategory.Reps:
                var simpleRep = measurement as RepsMeasurement;
                if (simpleRep!.Reps <= 0)
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            case ExerciseCategory.TimeEndurance:
                var simpleEndurance = measurement as TimeEnduranceMeasurement;
                if (simpleEndurance!.Time == 0)
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            case ExerciseCategory.TimeAndDistanceEndurance:
                var running = measurement as TimeDistanceEnduranceMeasurement;
                if (running!.Time == 0 || running.Distance <= 0)
                    throw new BadRequestException("No negative or zero measurements.");
                break;
            case ExerciseCategory.General:
                var general = measurement as GeneralMeasurement;
                // TODO: Add validation for every edge type. Like with bouldering,
                // TODO: max three letters (5a+), no higher than 9c, etc.
                // TODO: Add support for calculating both the French and American system? (5a == V2). Maybe a general
                //  system for supporting selecting multiple versions for fields.
                if (string.IsNullOrEmpty(general?.GeneralAchievement))
                    throw new BadRequestException("Please provide a valid value.");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ExerciseCategory), "This category is unknown.");
        }
    }
}

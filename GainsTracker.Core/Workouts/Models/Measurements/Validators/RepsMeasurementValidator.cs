using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Enums;

namespace GainsTracker.Core.Workouts.Models.Measurements.Validators;

public class RepsMeasurementValidator(WorkoutType type, Measurement previousBest, Measurement newMeasurement)
    : MeasurementValidator<RepsMeasurement>(type, previousBest, newMeasurement)
{
    public override bool CheckIfImproved() => NewMeasurement.Reps > PreviousBest.Reps;
}

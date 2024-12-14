using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.Core.Workouts.Models.Measurements.Validators;

public class StrengthMeasurementValidator(WorkoutType type, Measurement previousBest, Measurement newMeasurement)
    : MeasurementValidator<StrengthMeasurement>(type, previousBest, newMeasurement)
{
    public override bool CheckIfImproved() => NewMeasurement.Weight > PreviousBest.Weight;
}

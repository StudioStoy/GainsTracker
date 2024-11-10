using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.Core.Workouts.Models.Measurements.Validators;

public class StrengthMeasurementValidator : MeasurementValidator<StrengthMeasurement>
{
    public StrengthMeasurementValidator(WorkoutType type, Measurement previousBest, Measurement newMeasurement) 
        : base(type, previousBest, newMeasurement)
    {
    }

    public override bool CheckIfImproved()
    {
        return NewMeasurement.Weight > PreviousBest.Weight;
    }
}

#region

using GainsTracker.Common.Models.Workouts;

#endregion

namespace GainsTracker.Core.Workouts.Models.Measurements.Validators;

public class StrengthMeasurementValidator : MeasurementValidator<StrengthMeasurement>
{
    public StrengthMeasurementValidator(WorkoutType type, Measurement previousBest, Measurement newMeasurement)
        : base(type, previousBest, newMeasurement)
    {
    }

    public override bool CheckIfImproved() => NewMeasurement.Weight > PreviousBest.Weight;
}

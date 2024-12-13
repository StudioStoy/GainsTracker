#region

using GainsTracker.Common.Models.Workouts;

#endregion

namespace GainsTracker.Core.Workouts.Models.Measurements.Validators;

public class RepsMeasurementValidator : MeasurementValidator<RepsMeasurement>
{
    public RepsMeasurementValidator(WorkoutType type, Measurement previousBest, Measurement newMeasurement)
        : base(type, previousBest, newMeasurement)
    {
    }

    public override bool CheckIfImproved() => NewMeasurement.Reps > PreviousBest.Reps;
}

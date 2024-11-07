using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.Core.Components.Workouts.Models.Measurements.Validators;

public class RepsMeasurementValidator : MeasurementValidator<RepsMeasurement>
{
    public RepsMeasurementValidator(WorkoutType type, Measurement previousBest, Measurement newMeasurement) 
        : base(type, previousBest, newMeasurement)
    {
    }

    public override bool CheckIfImproved()
    {
        return NewMeasurement.Reps > PreviousBest.Reps;
    }
}

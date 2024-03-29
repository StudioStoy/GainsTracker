using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.Validators;

public class TimeMeasurementValidator : MeasurementValidator<TimeEnduranceMeasurement>
{
    public TimeMeasurementValidator(WorkoutType type, Measurement previousBest, Measurement newMeasurement) 
        : base(type, previousBest, newMeasurement)
    {
    }

    // TODO: welp, that sucks. Gotta somehow know if the user thinks doing longer times is an improvement. 
    public override bool CheckIfImproved()
    {
        switch (Type)
        {
            case WorkoutType.Planking:
            case WorkoutType.JumpingRope:
                return NewMeasurement.Time < PreviousBest.Time;
        }

        return false;
    }
}

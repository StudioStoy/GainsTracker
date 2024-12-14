using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.Core.Workouts.Models.Measurements.Validators;

public class TimeMeasurementValidator(WorkoutType type, Measurement previousBest, Measurement newMeasurement)
    : MeasurementValidator<TimeEnduranceMeasurement>(type, previousBest, newMeasurement)
{
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

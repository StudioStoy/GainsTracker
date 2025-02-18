using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Enums;

namespace GainsTracker.Core.Workouts.Models.Measurements.Validators;

public class TimeAndDistanceMeasurementValidator(WorkoutType type, Measurement previousBest, Measurement newMeasurement)
    : MeasurementValidator<TimeDistanceEnduranceMeasurement>(type, previousBest, newMeasurement)
{
    // TODO: Do something with time as well later. The user might have the same distance, but this time quicker.
    // However, the user might also try to increase endurance (and thus time) without doing more distance. Oh boy.
    public override bool CheckIfImproved() => NewMeasurement.Distance > PreviousBest.Distance;
}

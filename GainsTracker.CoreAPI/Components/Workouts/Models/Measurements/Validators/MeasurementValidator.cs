using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.Validators;

public abstract class MeasurementValidator<T> where T : Measurement
{
    protected readonly T NewMeasurement;
    protected readonly T PreviousBest;
    protected readonly WorkoutType Type;

    protected MeasurementValidator(WorkoutType type, Measurement previousBest, Measurement newMeasurement)
    {
        if (previousBest.Category != newMeasurement.Category)
            throw new ArgumentException("Can't compare measurements with different categories.");

        Type = type;
        PreviousBest = previousBest as T
                        ?? throw new NullReferenceException($"Could not convert {nameof(T)}");
        NewMeasurement = newMeasurement as T
                          ?? throw new NullReferenceException($"Could not convert {nameof(T)}");
    }

    public abstract bool CheckIfImproved();
}

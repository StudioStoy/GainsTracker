using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Enums;

namespace GainsTracker.Core.Workouts.Models.Measurements.Validators;

public abstract class MeasurementValidator
{
    public abstract bool CheckIfImproved();
}

public abstract class MeasurementValidator<T> : MeasurementValidator where T : Measurement
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
}

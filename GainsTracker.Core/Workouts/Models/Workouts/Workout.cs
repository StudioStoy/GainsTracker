using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Enums;
using GainsTracker.Core.Workouts.Models.Measurements;

namespace GainsTracker.Core.Workouts.Models.Workouts;

public class Workout
{
    public Workout() { }

    public Workout(Guid gainsAccountId, WorkoutType type, ExerciseCategory category, List<Measurement> measurements)
    {
        GainsAccountId = gainsAccountId;
        Type = type;
        Category = category;
        Measurements = measurements;
        if (measurements.Count != 0)
            PersonalBest = measurements.First();
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid GainsAccountId { get; init; }
    public WorkoutType Type { get; init; }
    public ExerciseCategory Category { get; init; }

    public Measurement? PersonalBest { get; set; }
    public List<Measurement> Measurements { get; init; } = [];

    public void AddNewMeasurement(Measurement measurement)
    {
        CheckAndUpdatePersonalBest(measurement, PersonalBest);
        Measurements.Add(measurement);
    }

    private void CheckAndUpdatePersonalBest(Measurement newMeasurement, Measurement? oldPersonalBest)
    {
        if (oldPersonalBest == null)
        {
            PersonalBest = newMeasurement;
            return;
        }

        if (newMeasurement.GetType() != oldPersonalBest.GetType())
            throw new ArgumentException("Cannot compare measurements as they're not the same type.");

        // Sets the PersonalBest to the new measurement if its values are higher, otherwise keep the old one.
        PersonalBest = MeasurementFactory.GetValidator(Type, oldPersonalBest, newMeasurement)
            .CheckIfImproved()
            ? newMeasurement
            : oldPersonalBest;
    }
}

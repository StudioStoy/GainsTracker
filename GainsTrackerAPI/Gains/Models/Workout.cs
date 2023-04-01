using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GainsTrackerAPI.Gains.Models.Measurements;

namespace GainsTrackerAPI.Gains.Models;

[Table("workout")]
[JsonDerivedType(typeof(EnduranceWorkout))]
[JsonDerivedType(typeof(PureRepWorkout))]
[JsonDerivedType(typeof(RunningWorkout))]
[JsonDerivedType(typeof(WeightWorkout))]
public abstract class Workout
{
    public List<Measurement> Measurements { get; set; } = new();

    public WorkoutType Type { get; set; }

    protected abstract void CheckPersonalBest<T>(Measurement newMeasurement, T oldPersonalBest);

    #region Relations

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string GainsAccountId { get; set; }

    #endregion
}

public class EnduranceWorkout : Workout
{
    public double PersonalBest { get; set; }

    protected override void CheckPersonalBest<T>(Measurement newMeasurement, T oldPersonalBest)
    {
        if (newMeasurement.GetType() != typeof(SimpleEnduranceMeasurement)) return;
        double newReps = (newMeasurement as SimpleEnduranceMeasurement)!.Time;

        if (newReps > PersonalBest)
            PersonalBest = newReps;
    }
}

public class PureRepWorkout : Workout
{
    public int PersonalBest { get; set; }

    protected override void CheckPersonalBest<T>(Measurement newMeasurement, T oldPersonalBest)
    {
        if (newMeasurement.GetType() != typeof(SimpleRepMeasurement)) return;
        int newReps = (newMeasurement as SimpleRepMeasurement)!.Reps;

        if (newReps > PersonalBest)
            PersonalBest = newReps;
    }
}

public class RunningWorkout : Workout
{
    public double PersonalBest { get; set; }

    protected override void CheckPersonalBest<T>(Measurement newMeasurement, T oldPersonalBest)
    {
        throw new NotImplementedException();
    }
}

public class WeightWorkout : Workout
{
    public double PersonalBest { get; set; }

    protected override void CheckPersonalBest<T>(Measurement newMeasurement, T oldPersonalBest)
    {
        if (newMeasurement.GetType() != typeof(WeightMeasurement)) return;
        double newWeight = (newMeasurement as WeightMeasurement)!.Weight;

        if (newWeight > PersonalBest)
            PersonalBest = newWeight;
    }
}

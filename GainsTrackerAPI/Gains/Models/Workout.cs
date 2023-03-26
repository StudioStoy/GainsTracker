using System.ComponentModel.DataAnnotations.Schema;
using GainsTrackerAPI.Gains.Models.Measurements;

namespace GainsTrackerAPI.Gains.Models;

[Table("workout")]
public abstract class Workout
{
    public WorkoutType Type { get; set; }
    public List<Measurement> Measurements { get; set; } = new();

    #region Relations

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string GainsAccountId { get; set; }

    #endregion
}

public class EnduranceWorkout : Workout
{
    public double PersonalBest { get; set; }
}

public class PureRepWorkout : Workout
{
    public int PersonalBest { get; set; }
}

public class RunningWorkout : Workout
{
    public double PersonalBest { get; set; }
}

public class WeightWorkout : Workout
{
    public double PersonalBest { get; set; }
}

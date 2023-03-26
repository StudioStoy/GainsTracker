namespace GainsTrackerAPI.Gains.Models;

public class WorkoutFactory
{
    private readonly WorkoutType _workoutType;

    public WorkoutFactory(WorkoutType type)
    {
        _workoutType = type;
    }

    public Workout CreateWorkout()
    {
        switch (_workoutType)
        {
            case WorkoutType.Squat:
            case WorkoutType.Abduction:
            case WorkoutType.Adduction:
            case WorkoutType.BenchPress:
            case WorkoutType.CalfExtensions:
            case WorkoutType.HackSquat:
            case WorkoutType.LegPress:
            case WorkoutType.ShoulderPress:
            case WorkoutType.DumbbellPress:
            case WorkoutType.DumbbellCurl:
                return new WeightWorkout();
            case WorkoutType.ClosePullUp:
            case WorkoutType.WidePullUp:
            case WorkoutType.DiamondPushUp:
            case WorkoutType.ClosePushUp:
            case WorkoutType.WidePushUp:
                return new PureRepWorkout();
            case WorkoutType.Planking:
            case WorkoutType.JumpingJacks:
            case WorkoutType.JumpingRope:
            case WorkoutType.Walking:
                return new EnduranceWorkout();
            case WorkoutType.Running:
                return new RunningWorkout();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

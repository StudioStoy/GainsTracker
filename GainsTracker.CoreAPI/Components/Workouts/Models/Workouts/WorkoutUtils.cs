using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;

public static class WorkoutUtils
{
    public static ExerciseCategory GetCategoryFromType(string workoutType)
    {
        return GetCategoryFromType(Enum.Parse<WorkoutType>(workoutType));
    }

    public static ExerciseCategory GetCategoryFromType(this WorkoutType workoutType)
    {
        switch (workoutType)
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
                return ExerciseCategory.Strength;
            case WorkoutType.ClosePullUp:
            case WorkoutType.WidePullUp:
            case WorkoutType.DiamondPushUp:
            case WorkoutType.ClosePushUp:
            case WorkoutType.WidePushUp:
                return ExerciseCategory.SimpleRep;
            case WorkoutType.Planking:
            case WorkoutType.JumpingJacks:
            case WorkoutType.JumpingRope:
            case WorkoutType.Walking:
                return ExerciseCategory.SimpleEndurance;
            case WorkoutType.Running:
                return ExerciseCategory.RunningEndurance;
            default:
                throw new ArgumentOutOfRangeException(null, "This type is not supported.");
        }
    }
}

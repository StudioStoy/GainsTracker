using GainsTracker.Common.Exceptions;
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
            case WorkoutType.LatPullDown:
            case WorkoutType.BicepPullDown:
            case WorkoutType.PectoralFly:
            case WorkoutType.LowRows:
            case WorkoutType.DeadLift:
            case WorkoutType.HammerCurl:
                return ExerciseCategory.Strength;
            case WorkoutType.ClosePullUp:
            case WorkoutType.WidePullUp:
            case WorkoutType.DiamondPushUp:
            case WorkoutType.ClosePushUp:
            case WorkoutType.WidePushUp:
                return ExerciseCategory.Reps;
            case WorkoutType.Planking:
            case WorkoutType.JumpingJacks:
            case WorkoutType.JumpingRope:
                return ExerciseCategory.TimeEndurance;
            case WorkoutType.Walking:
            case WorkoutType.Running:
            case WorkoutType.Cycling:
                return ExerciseCategory.TimeAndDistanceEndurance;
            case WorkoutType.Bouldering:
                return ExerciseCategory.General;
            default:
                throw new NotFoundException($"Type {workoutType} is not supported.");
        }
    }
}

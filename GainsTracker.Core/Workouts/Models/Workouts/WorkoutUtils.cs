#region

using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.Workouts;

#endregion

namespace GainsTracker.Core.Workouts.Models.Workouts;

public static class WorkoutUtils
{
    public static ExerciseCategory GetCategoryFromType(string workoutType) =>
        GetCategoryFromType(Enum.Parse<WorkoutType>(workoutType));

    public static ExerciseCategory GetCategoryFromType(this WorkoutType workoutType)
    {
        switch (workoutType)
        {
            case WorkoutType.WeightedSquat:
            case WorkoutType.Abduction:
            case WorkoutType.Adduction:
            case WorkoutType.BenchPress:
            case WorkoutType.CalfExtensions:
            case WorkoutType.LegPress:
            case WorkoutType.ShoulderPress:
            case WorkoutType.DumbbellPress:
            case WorkoutType.BicepCurl:
            case WorkoutType.LatPullDown:
            case WorkoutType.BicepPullDown:
            case WorkoutType.PectoralFly:
            case WorkoutType.LowRows:
            case WorkoutType.DeadLift:
            case WorkoutType.ChestPress:
            case WorkoutType.HammerCurl:
            case WorkoutType.InclineBenchPress:
            case WorkoutType.InclineDumbbellPress:
            case WorkoutType.Shrugs:
            case WorkoutType.LegCurl:
            case WorkoutType.LegExtension:
            case WorkoutType.OverheadPress:
                return ExerciseCategory.Strength;
            case WorkoutType.ClosePullUp:
            case WorkoutType.WidePullUp:
            case WorkoutType.DiamondPushUp:
            case WorkoutType.ClosePushUp:
            case WorkoutType.BodySquat:
            case WorkoutType.WidePushUp:
            case WorkoutType.SitUps:
            case WorkoutType.LegRaise:
                return ExerciseCategory.Reps;
            case WorkoutType.Planking:
            case WorkoutType.JumpingRope:
                return ExerciseCategory.TimeEndurance;
            case WorkoutType.Walking:
            case WorkoutType.Running:
            case WorkoutType.Cycling:
            case WorkoutType.Swimming:
            case WorkoutType.Rowing:
                return ExerciseCategory.TimeAndDistanceEndurance;
            case WorkoutType.Bouldering:
                return ExerciseCategory.General;
            default:
                throw new NotFoundException($"Type {workoutType} is not supported.");
        }
    }
}

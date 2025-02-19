using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Enums;

namespace GainsTracker.Core.Workouts.Models.Workouts;

public static class WorkoutUtils
{
    public static ExerciseCategory GetCategory(string workoutType) =>
        GetCategory(Enum.Parse<WorkoutType>(workoutType));

    public static ExerciseCategory GetCategory(this WorkoutType workoutType) =>
        workoutType switch
        {
            WorkoutType.WeightedSquat or WorkoutType.Abduction or WorkoutType.Adduction or WorkoutType.BenchPress
                or WorkoutType.CalfExtensions or WorkoutType.LegPress or WorkoutType.ShoulderPress
                or WorkoutType.DumbbellPress or WorkoutType.BicepCurl or WorkoutType.LatPullDown
                or WorkoutType.BicepPullDown or WorkoutType.PectoralFly or WorkoutType.LowRows or WorkoutType.DeadLift
                or WorkoutType.ChestPress or WorkoutType.HammerCurl or WorkoutType.InclineBenchPress
                or WorkoutType.InclineDumbbellPress or WorkoutType.Shrugs or WorkoutType.LegCurl
                or WorkoutType.LegExtension or WorkoutType.OverheadPress => ExerciseCategory.Strength,
            WorkoutType.ClosePullUp or WorkoutType.WidePullUp or WorkoutType.DiamondPushUp or WorkoutType.ClosePushUp
                or WorkoutType.BodySquat or WorkoutType.WidePushUp or WorkoutType.SitUps
                or WorkoutType.LegRaise => ExerciseCategory.Reps,
            WorkoutType.Planking or WorkoutType.JumpingRope => ExerciseCategory.TimeEndurance,
            WorkoutType.Walking or WorkoutType.Running or WorkoutType.Cycling or WorkoutType.Swimming
                or WorkoutType.Rowing => ExerciseCategory.TimeDistanceEndurance,
            WorkoutType.Bouldering => ExerciseCategory.General,
            _ => throw new NotFoundException($"Type {workoutType} is not supported."),
        };
}

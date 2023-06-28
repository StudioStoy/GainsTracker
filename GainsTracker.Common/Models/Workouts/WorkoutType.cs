using System.Text.Json.Serialization;

namespace GainsTracker.Common.Models.Workouts;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WorkoutType
{
    ClosePullUp,
    WidePullUp,
    LatPullDown,
    BicepPullDown,
    PectoralFly,
    LowRows,

    Squat,
    HackSquat,
    LegPress,
    Abduction,
    Adduction,
    CalfExtensions,
    DeadLift,

    DiamondPushUp,
    ClosePushUp,
    WidePushUp,
    ShoulderPress,
    BenchPress,
    DumbbellPress,
    DumbbellCurl,
    HammerCurl,

    // Time
    Planking,
    JumpingJacks,
    JumpingRope,

    // Time and distance
    Running,
    Walking,
    Cycling,

    // General
    Bouldering
}

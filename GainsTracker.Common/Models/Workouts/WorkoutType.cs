using System.Text.Json.Serialization;

namespace GainsTracker.Common.Models.Workouts;

// TODO: For the strength & rep types, add support for saying if it is only one arm or for both.
// TODO: For the endurance and time, add support for adding TopSpeed.
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WorkoutType
{
    ClosePullUp,
    WidePullUp,
    LatPullDown,
    BicepPullDown,
    PectoralFly,
    LowRows,

    WeightedSquat,
    BodySquat,
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
    ChestPress,
    DumbbellPress,
    BicepCurl,
    HammerCurl,

    // Time
    Planking,
    JumpingRope,

    // Time and distance + top speed
    Running,
    Walking,
    Cycling,
    Swimming,
    Rowing,

    // General
    Bouldering
}

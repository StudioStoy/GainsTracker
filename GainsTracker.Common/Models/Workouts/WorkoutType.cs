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
    ClosePushUp,
    WidePushUp,
    ShoulderPress,
    BenchPress,
    ChestPress,
    DumbbellPress,
    BicepCurl,
    HammerCurl,
    Planking,
    Running,
    Walking,
    Cycling,
    Swimming,
    Rowing,
    Bouldering,
    JumpingRope,
    DeadLift,
    CalfExtensions,
    InclineBenchPress,
    DiamondPushUp,
    InclineDumbbellPress,
    Shrugs,
    OverheadPress,
    LegExtension,
    LegCurl,
    SitUps,
    LegRaise
}

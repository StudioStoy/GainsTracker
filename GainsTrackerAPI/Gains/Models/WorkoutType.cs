using System.Text.Json.Serialization;

namespace GainsTrackerAPI.Gains.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WorkoutType
{
    ClosePullUp,
    WidePullUp,

    Squat,
    HackSquat,
    LegPress,
    Abduction,
    Adduction,
    CalfExtensions,

    DiamondPushUp,
    ClosePushUp,
    WidePushUp,
    ShoulderPress,
    BenchPress,
    DumbbellPress,
    DumbbellCurl,

    Planking,
    JumpingJacks,
    JumpingRope,

    Running,
    Walking
}

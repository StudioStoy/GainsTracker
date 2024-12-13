#region

using System.Text.Json.Serialization;

#endregion

namespace GainsTracker.Common.Models.Workouts;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExerciseCategory
{
    Strength,
    Reps,
    TimeEndurance,
    TimeAndDistanceEndurance,
    General,
}

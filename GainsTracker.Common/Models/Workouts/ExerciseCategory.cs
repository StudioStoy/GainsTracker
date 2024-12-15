using System.Text.Json.Serialization;

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

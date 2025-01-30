using System.Text.Json.Serialization;

namespace GainsTracker.Common.Models.Workouts.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExerciseCategory
{
    Strength,
    Reps,
    TimeEndurance,
    TimeDistanceEndurance,
    General,
}

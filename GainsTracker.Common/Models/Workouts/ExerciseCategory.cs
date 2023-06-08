using System.Text.Json.Serialization;

namespace GainsTracker.Common.Models.Workouts;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExerciseCategory
{
    Strength,
    SimpleRep,
    SimpleEndurance,
    RunningEndurance
}

using System.Text.Json.Serialization;

namespace GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExerciseCategory
{
    Strength,
    SimpleRep,
    SimpleEndurance,
    RunningEndurance
}

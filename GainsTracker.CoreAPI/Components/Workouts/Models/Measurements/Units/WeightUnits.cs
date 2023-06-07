using System.Text.Json.Serialization;

namespace GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.Units;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum WeightUnits
{
    Kilograms,
    Grams
}

using System.Text.Json.Serialization;

namespace GainsTracker.Common.Models.Measurements.Enums.Units;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DistanceUnits
{
    Kilometers,
    Meters,
}

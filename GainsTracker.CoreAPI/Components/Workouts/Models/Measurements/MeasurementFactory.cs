using System.Text.Json;
using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;

public static class MeasurementFactory
{
    public static Measurement DeserializeMeasurementFromJson(ExerciseCategory category, JsonDocument? measurementData)
    {
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        if (measurementData == null)
            throw new ArgumentException("Can't deserialize measurement, invalid data provided.");

        return (category switch
        {
            ExerciseCategory.Strength => measurementData.Deserialize<StrengthMeasurement>(options),
            ExerciseCategory.TimeAndDistanceEndurance => measurementData.Deserialize<TimeAndDistanceEnduranceMeasurement>(options),
            ExerciseCategory.TimeEndurance => measurementData.Deserialize<TimeEnduranceMeasurement>(options),
            ExerciseCategory.Reps => measurementData.Deserialize<RepsMeasurement>(options),
            _ => throw new NotImplementedException()
        })!;
    }

    public static JsonDocument SerializeMeasurementToJson(Measurement measurement)
    {
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.SerializeToDocument(measurement, measurement.GetType(), options);
    }
}

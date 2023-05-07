﻿using System.Text.Json;
using GainsTrackerAPI.Components.Gains.Models.Workouts;

namespace GainsTrackerAPI.Components.Gains.Models.Measurements;

public static class MeasurementFactory
{
    public static Measurement DeserializeMeasurementFromJson(ExerciseCategory category, JsonDocument measurementData)
    {
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        return (category switch
        {
            ExerciseCategory.Strength => measurementData.Deserialize<StrengthMeasurement>(options),
            ExerciseCategory.RunningEndurance => measurementData.Deserialize<RunningEnduranceMeasurement>(options),
            ExerciseCategory.SimpleEndurance => measurementData.Deserialize<SimpleEnduranceMeasurement>(options),
            ExerciseCategory.SimpleRep => measurementData.Deserialize<SimpleRepMeasurement>(options),
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
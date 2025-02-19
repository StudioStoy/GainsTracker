using GainsTracker.Common.Models.Workouts.Measurements;

namespace GainsTracker.Common.Models.Workouts;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CreateMeasurementDtoConverter : JsonConverter<CreateMeasurementDto>
{
    public override CreateMeasurementDto Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        if (!root.TryGetProperty("Category", out var categoryElement))
            throw new JsonException("Missing 'Category' property for polymorphic deserialization.");

        var category = categoryElement.GetString();
        var targetType = category switch
        {
            "Reps" => typeof(CreateRepsMeasurementDto),
            "Strength" => typeof(CreateStrengthMeasurementDto),
            "TimeDistanceEndurance" => typeof(CreateTimeDistanceEnduranceMeasurementDto),
            "TimeEndurance" => typeof(CreateTimeEnduranceMeasurementDto),
            "General" => typeof(CreateGeneralMeasurementDto),
            _ => throw new JsonException($"Unknown measurement category '{category}'"),
        };

        return (CreateMeasurementDto)JsonSerializer.Deserialize(root.GetRawText(), targetType, options)!;
    }

    public override void Write(Utf8JsonWriter writer, CreateMeasurementDto value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}

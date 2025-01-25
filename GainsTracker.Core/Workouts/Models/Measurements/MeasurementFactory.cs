using System.Text.Json;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Enums;
using GainsTracker.Common.Models.Workouts.Measurements;
using GainsTracker.Core.Workouts.Models.Measurements.Validators;

namespace GainsTracker.Core.Workouts.Models.Measurements;

public static class MeasurementFactory
{
    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    public static Measurement DeserializeMeasurementFromJson(ExerciseCategory category, JsonDocument? measurementData)
    {
        if (measurementData == null)
            throw new ArgumentException("Can't deserialize measurement, invalid data provided.");

        return (category switch
        {
            ExerciseCategory.Strength => measurementData.Deserialize<StrengthMeasurement>(Options),
            ExerciseCategory.TimeAndDistanceEndurance => measurementData
                .Deserialize<TimeDistanceEnduranceMeasurement>(Options),
            ExerciseCategory.TimeEndurance => measurementData.Deserialize<TimeEnduranceMeasurement>(Options),
            ExerciseCategory.Reps => measurementData.Deserialize<RepsMeasurement>(Options),
            ExerciseCategory.General => measurementData.Deserialize<GeneralMeasurement>(Options),
            _ => throw new ArgumentOutOfRangeException(nameof(category), "That exercise is not supported"),
        })!;
    }

    public static JsonDocument SerializeMeasurementToJson(Measurement measurement) =>
        JsonSerializer.SerializeToDocument(measurement, measurement.GetType(), Options);

    public static MeasurementValidator GetValidator(WorkoutType type, Measurement previousBest,
        Measurement newMeasurement)
    {
        Dictionary<Type, Type> validatorMap = new()
        {
            { typeof(RepsMeasurement), typeof(RepsMeasurementValidator) },
            { typeof(StrengthMeasurement), typeof(StrengthMeasurementValidator) },
            { typeof(TimeEnduranceMeasurement), typeof(TimeMeasurementValidator) },
            { typeof(TimeDistanceEnduranceMeasurement), typeof(TimeAndDistanceMeasurementValidator) },
            { typeof(GeneralMeasurement), typeof(GeneralMeasurementValidator) },
        };

        if (validatorMap.TryGetValue(previousBest.GetType(), out var validatorType))
        {
            var validatorInstance = Activator.CreateInstance(validatorType, type, previousBest, newMeasurement);
            if (validatorInstance is MeasurementValidator result)
                return result;
        }

        throw new ArgumentOutOfRangeException(nameof(previousBest), previousBest,
            "MeasurementValidator could not be instantiated");
    }
}

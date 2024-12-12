using System.Text.Json;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Core.Workouts.Models.Measurements.Validators;

namespace GainsTracker.Core.Workouts.Models.Measurements;

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
            ExerciseCategory.TimeAndDistanceEndurance => measurementData
                .Deserialize<TimeAndDistanceEnduranceMeasurement>(options),
            ExerciseCategory.TimeEndurance => measurementData.Deserialize<TimeEnduranceMeasurement>(options),
            ExerciseCategory.Reps => measurementData.Deserialize<RepsMeasurement>(options),
            ExerciseCategory.General => measurementData.Deserialize<GeneralMeasurement>(options),
            _ => throw new ArgumentOutOfRangeException(nameof(category), "That exercise is not supported")
        })!;
    }

    public static JsonDocument SerializeMeasurementToJson(Measurement measurement)
    {
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.SerializeToDocument(measurement, measurement.GetType()!, options);
    }

    public static MeasurementValidator GetValidator<T>(WorkoutType type, Measurement previousBest,
        Measurement newMeasurement) where T : Measurement
    {
        Dictionary<Type, Type> validatorMap = new()
        {
            { typeof(RepsMeasurement), typeof(RepsMeasurementValidator) },
            { typeof(StrengthMeasurement), typeof(StrengthMeasurementValidator) },
            { typeof(TimeEnduranceMeasurement), typeof(TimeMeasurementValidator) },
            { typeof(TimeAndDistanceEnduranceMeasurement), typeof(TimeAndDistanceMeasurementValidator) },
            { typeof(GeneralMeasurement), typeof(GeneralMeasurementValidator) }
        };

        if (validatorMap.TryGetValue(previousBest.GetType()!, out Type? validatorType))
        {
            var validatorInstance = Activator.CreateInstance(validatorType, type, previousBest, newMeasurement);
            if (validatorInstance is MeasurementValidator result)
                return result;
        }

        throw new ArgumentOutOfRangeException(nameof(previousBest), previousBest,
            "MeasurementValidator could not be instantiated");
    }
}

using System.Text.Json;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.CoreAPI.Components.Workouts.Models.Measurements.Validators;

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
            ExerciseCategory.General => measurementData.Deserialize<GeneralMeasurement>(options),
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

    public static MeasurementValidator<T> GetValidator<T>(WorkoutType type, Measurement previousBest, Measurement newMeasurement) where T : Measurement
    {
        Dictionary<Type, Type> validatorMap = new()
        {
            { typeof(RepsMeasurement), typeof(RepsMeasurementValidator) },
            { typeof(StrengthMeasurement), typeof(RepsMeasurementValidator) },
            { typeof(TimeEnduranceMeasurement), typeof(TimeMeasurementValidator) },
            { typeof(TimeAndDistanceEnduranceMeasurement), typeof(TimeAndDistanceMeasurementValidator) },
            { typeof(GeneralMeasurement), typeof(GeneralMeasurementValidator) }
        };

        if (validatorMap.TryGetValue(typeof(T), out Type? validatorType))
        {
            return Activator.CreateInstance(type: validatorType, type, previousBest, newMeasurement) 
                       as MeasurementValidator<T> ?? throw new InvalidOperationException();
        }

        throw new ArgumentOutOfRangeException(nameof(previousBest), previousBest, "MeasurementValidator could not be instantiated");
    }
}

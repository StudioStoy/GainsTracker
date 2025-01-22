using System.Text.Json;
using GainsTracker.Common.Models.Measurements.Units;

namespace GainsTracker.Common.Models.Workouts.Dto;

public record MeasurementDto(
    string Id,
    string WorkoutId,
    ExerciseCategory Category,
    DateTime TimeOfRecord,
    string Notes,
    JsonDocument? Data
);

public record StrengthMeasurementDto(
    WeightUnits WeightUnit,
    double Weight,
    int Reps
);

public record TimeDistanceEnduranceMeasurementDto(
    DistanceUnits DistanceUnit,
    double Distance,
    string Time = "00:00:00"
);

public record TimeEnduranceMeasurementDto(double Time);

public record RepsMeasurementDto(int Reps);

public record GeneralMeasurementDto(string General);

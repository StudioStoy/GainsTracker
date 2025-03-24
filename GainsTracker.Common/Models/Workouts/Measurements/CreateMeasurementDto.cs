using System.Text.Json.Serialization;
using GainsTracker.Common.Models.Measurements.Enums.Units;
using GainsTracker.Common.Models.Workouts.Enums;

namespace GainsTracker.Common.Models.Workouts.Measurements;

[JsonConverter(typeof(CreateMeasurementDtoConverter))]
public record CreateMeasurementDto
{
    public ExerciseCategory Category { get; init; }
    public string Notes { get; set; } = string.Empty;
}

public record CreateStrengthMeasurementDto : CreateMeasurementDto
{
    public WeightUnits WeightUnit { get; set; }
    public double Weight { get; set; } = 0.0;
    public int Reps { get; set; } = 0;
}

public record CreateTimeDistanceEnduranceMeasurementDto : CreateMeasurementDto
{
    public DistanceUnits DistanceUnit { get; set; }
    public double Distance { get; set; } = 0.0;
    public long Time { get; set; }
}

public record CreateTimeEnduranceMeasurementDto : CreateMeasurementDto
{
    public long Time { get; set; }
}

public record CreateRepsMeasurementDto : CreateMeasurementDto
{
    public int Reps { get; set; } = 0;
}

public record CreateGeneralMeasurementDto : CreateMeasurementDto
{
    public string General { get; set; } = string.Empty;
}

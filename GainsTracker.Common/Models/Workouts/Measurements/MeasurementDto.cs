using GainsTracker.Common.Models.Measurements.Enums.Units;
using GainsTracker.Common.Models.Workouts.Enums;

namespace GainsTracker.Common.Models.Workouts.Measurements;

public interface IMeasurementDto
{
    Guid Id { get; init; }
}

public class MeasurementDto : IMeasurementDto
{
    public Guid Id { get; init; } = Guid.Empty;
    public string WorkoutId { get; init; } = string.Empty;
    public ExerciseCategory Category { get; init; }
    public DateTime TimeOfRecord { get; init; }
    public string Notes { get; init; } = string.Empty;
}

public class StrengthMeasurementDto : MeasurementDto
{
    public WeightUnits WeightUnit { get; init; }
    public double Weight { get; init; } = 0.0;
    public int Reps { get; init; } = 0;
}

// TODO: Check if the Time property type should be changed to numbers.
public class TimeDistanceEnduranceMeasurementDto : MeasurementDto
{
    public DistanceUnits DistanceUnit { get; init; }
    public double Distance { get; init; } = 0.0;
    public string Time { get; init; } = "00:00:00";
}

public class TimeEnduranceMeasurementDto : MeasurementDto
{
    public string Time { get; init; } = "00:00:00";
}

public class RepsMeasurementDto : MeasurementDto
{
    public int Reps { get; init; } = 0;
}

public class GeneralMeasurementDto : MeasurementDto
{
    public string General { get; init; } = string.Empty;
}

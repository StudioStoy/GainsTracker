namespace GainsTracker.Common.Models.Workouts.Dto;

public class WorkoutMeasurementsDto
{
    public string Id { get; set; } = "";
    public List<MeasurementDto> Measurements { get; set; } = new();
}

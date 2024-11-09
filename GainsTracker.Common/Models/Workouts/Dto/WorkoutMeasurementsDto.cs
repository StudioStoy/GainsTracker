namespace GainsTracker.Common.Models.Workouts.Dto;

public class WorkoutMeasurementsDto
{
    public Guid Id { get; set; }
    public List<MeasurementDto> Measurements { get; set; } = [];
}

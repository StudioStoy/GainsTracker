using GainsTracker.CoreAPI.Components.Gains.Models.Measurements;
using GainsTracker.CoreAPI.Components.Gains.Models.Workouts;

namespace GainsTracker.CoreAPI.Components.Gains.Services.Dto;

public class WorkoutMeasurementsDto
{
    public string Id { get; set; } = "";
    public List<MeasurementDto> Measurements { get; set; } = new();

    public static WorkoutMeasurementsDto FromWorkout(Workout workout)
    {
        return new WorkoutMeasurementsDto
        {
            Id = workout.Id,
            Measurements = workout.Measurements
                .Select(m => new MeasurementDto
                {
                    WorkoutId = m.WorkoutId,
                    Category = m.Category,
                    TimeOfRecord = m.TimeOfRecord,
                    Data = MeasurementFactory.SerializeMeasurementToJson(m)
                }).ToList()
        };
    }
}

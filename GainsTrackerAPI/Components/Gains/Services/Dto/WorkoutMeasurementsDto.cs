using GainsTrackerAPI.Components.Gains.Models.Measurements;
using GainsTrackerAPI.Components.Gains.Models.Workouts;

namespace GainsTrackerAPI.Components.Gains.Services.Dto;

public class WorkoutMeasurementsDto
{
    public string Id { get; set; } = "";
    public List<MeasurementResponseDto> Measurements { get; set; } = new();

    public static WorkoutMeasurementsDto FromWorkout(Workout workout)
    {
        return new WorkoutMeasurementsDto
        {
            Id = workout.Id,
            Measurements = workout.Measurements
                .Select(m => new MeasurementResponseDto
                {
                    WorkoutId = m.WorkoutId,
                    Category = m.Category,
                    TimeOfRecord = m.TimeOfRecord,
                    Data = MeasurementFactory.SerializeMeasurementToJson(m)
                }).ToList()
        };
    }
}

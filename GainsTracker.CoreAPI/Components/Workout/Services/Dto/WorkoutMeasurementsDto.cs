using GainsTracker.CoreAPI.Components.Workout.Models.Measurements;

namespace GainsTracker.CoreAPI.Components.Workout.Services.Dto;

public class WorkoutMeasurementsDto
{
    public string Id { get; set; } = "";
    public List<MeasurementDto> Measurements { get; set; } = new();

    public static WorkoutMeasurementsDto FromWorkout(Models.Workouts.Workout workout)
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

using GainsTracker.CoreAPI.Components.Workout.Models.Measurements;
using GainsTracker.CoreAPI.Components.Workout.Models.Workouts;

namespace GainsTracker.CoreAPI.Components.Workout.Services.Dto;

public class WorkoutDto
{
    private WorkoutDto(string gainsAccountId)
    {
        GainsAccountId = gainsAccountId;
    }

    public string Id { get; set; } = "";
    public string GainsAccountId { get; set; }
    public WorkoutType WorkoutType { get; set; }
    public MeasurementDto? PersonalBest { get; set; }

    public static WorkoutDto FromWorkout(Models.Workouts.Workout workout)
    {
        MeasurementDto? bestMeasurement = null;

        if (workout.PersonalBest != null)
            bestMeasurement = new MeasurementDto
            {
                WorkoutId = workout.PersonalBest.WorkoutId,
                TimeOfRecord = workout.PersonalBest.TimeOfRecord,
                Category = workout.PersonalBest.Category,
                Data = MeasurementFactory.SerializeMeasurementToJson(workout.PersonalBest)
            };

        return new WorkoutDto(workout.GainsAccountId)
        {
            Id = workout.Id,
            WorkoutType = workout.WorkoutType,
            PersonalBest = bestMeasurement
        };
    }
}

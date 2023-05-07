using GainsTrackerAPI.Components.Gains.Models.Measurements;
using GainsTrackerAPI.Components.Gains.Models.Workouts;

namespace GainsTrackerAPI.Components.Gains.Services.Dto;

public class WorkoutDto
{
    public string Id { get; set; } = "";
    public string GainsAccountId { get; set; }
    public WorkoutType WorkoutType { get; set; }
    public MeasurementDto? PersonalBest { get; set; }

    public static WorkoutDto FromWorkout(Workout workout)
    {
        MeasurementDto? bestMeasurement = null;

        if (workout.PersonalBest != null)
        {
            bestMeasurement = new MeasurementDto();
            bestMeasurement.WorkoutId = workout.PersonalBest.WorkoutId;
            bestMeasurement.TimeOfRecord = workout.PersonalBest.TimeOfRecord;
            bestMeasurement.Category = workout.PersonalBest.Category;
            bestMeasurement.Data = MeasurementFactory.SerializeMeasurementToJson(workout.PersonalBest);
        }

        return new WorkoutDto
        {
            Id = workout.Id,
            GainsAccountId = workout.GainsAccountId,
            WorkoutType = workout.WorkoutType,
            PersonalBest = bestMeasurement
        };
    }
}

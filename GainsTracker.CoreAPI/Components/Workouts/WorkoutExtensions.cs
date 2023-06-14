using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;

namespace GainsTracker.CoreAPI.Components.Workouts;

public static class WorkoutExtensions
{
    public static WorkoutDto ToDto(this Workout workout)
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
            Type = workout.Type,
            Category = workout.Category,
            PersonalBest = bestMeasurement
        };
    }
    
    public static WorkoutMeasurementsDto ToMeasurementsListDto(this Workout workout)
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

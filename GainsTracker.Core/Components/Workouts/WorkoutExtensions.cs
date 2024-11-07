using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.Components.Workouts.Models.Measurements;
using GainsTracker.Core.Components.Workouts.Models.Workouts;

namespace GainsTracker.Core.Components.Workouts;

public static class WorkoutExtensions
{
    public static WorkoutMeasurementsDto ToMeasurementsListDto(this Workout workout)
    {
        return new WorkoutMeasurementsDto
        {
            Id = workout.Id,
            Measurements = workout.Measurements
                .Select(m => new MeasurementDto
                {
                    Id = m.Id,
                    WorkoutId = m.WorkoutId,
                    Category = m.Category,
                    TimeOfRecord = m.TimeOfRecord,
                    Notes = m.Notes,
                    Data = MeasurementFactory.SerializeMeasurementToJson(m)
                }).ToList()
        };
    }
}

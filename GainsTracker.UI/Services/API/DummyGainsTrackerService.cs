using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.UI.Services;

public class DummyGainsTrackerService : IGainsTrackerService
{
    public Task<List<WorkoutDto>> GetUserWorkouts()
    {
        return new Task<List<WorkoutDto>>(CreateDummyWorkoutData);
    }

    public Task<List<MeasurementDto>> GetPersonalBests()
    {
        throw new NotImplementedException();
    }

    #region Dummy Workout Data

    private static List<WorkoutDto> CreateDummyWorkoutData()
    {
        WorkoutDto workout1 = new(Guid.NewGuid())
        {
            Id = Guid.NewGuid(),
            Category = ExerciseCategory.Strength,
            Type = WorkoutType.WeightedSquat,
            PersonalBest = new MeasurementDto(),
        };

        return new List<WorkoutDto> { workout1 };
    }

    #endregion
}

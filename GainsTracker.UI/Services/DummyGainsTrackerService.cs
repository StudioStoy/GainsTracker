using GainsTracker.Common.Models.Workouts;
using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.UI.Services;

// TODO: Provide more useful dummy data in these methods
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

    private List<WorkoutDto> CreateDummyWorkoutData()
    {
        WorkoutDto workout1 = new("stije-id")
        {
            Id = "workout-1",
            Category = ExerciseCategory.Strength,
            Type = WorkoutType.Squat,
            PersonalBest = new MeasurementDto(),
        };

        return new List<WorkoutDto> { workout1 };
    }

    #endregion
}

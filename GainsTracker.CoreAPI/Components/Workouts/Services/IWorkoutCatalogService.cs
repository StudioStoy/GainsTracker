using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.CoreAPI.Components.Workouts.Services;

public interface IWorkoutCatalogService
{
    List<WorkoutTypeDto> GetAvailableWorkoutTypesForUser(string username);
    List<WorkoutTypeDto> GetAllWorkoutTypes();
}

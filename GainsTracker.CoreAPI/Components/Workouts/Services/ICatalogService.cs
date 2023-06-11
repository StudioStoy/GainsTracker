using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.CoreAPI.Components.Workouts.Services;

public interface ICatalogService
{
    List<WorkoutTypeDto> GetAvailableWorkoutTypesForUser(string username);
}

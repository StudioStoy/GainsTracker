using GainsTracker.Common.Models.Workouts.Dto;

namespace GainsTracker.Core.Components.Workouts.Interfaces.Services;

public interface ICatalogService
{
    Task<List<WorkoutTypeDto>> GetAvailableWorkoutTypesForUser(string username);
}

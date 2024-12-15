using System.Diagnostics;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.UI.Services.API.Interfaces;
using static GainsTracker.Common.Constants;

namespace GainsTracker.UI.Services.API;

// All the code in this file is included in all platforms.
public class GainsTrackerService(HttpClient httpClient) : IGainsTrackerService
{
    public async Task<List<WorkoutDto>> GetUserWorkouts()
    {
        var url = $"{BaseUrl}/gains/user/workout";
        try
        {
            var response = await httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();


            return []; // TODO: fix, obviously.
        }
        catch (Exception ex)
        {
            Debug.WriteLine("uh oh:");
            Debug.WriteLine(ex);
            return [];
        }
    }

    public async Task<List<MeasurementDto>> GetPersonalBests()
    {
        var workouts = await GetUserWorkouts();
        List<MeasurementDto> personalBests = workouts
            .Where(workout => workout.PersonalBest != null)
            .Select(workout => workout.PersonalBest)
            .ToList()!;

        return personalBests;
    }
}

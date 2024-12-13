#region

using System.Diagnostics;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.UI.Services.API.Interfaces;
using static GainsTracker.Common.Constants;

#endregion

namespace GainsTracker.UI.Services.API;

// All the code in this file is included in all platforms.
public class GainsTrackerService : IGainsTrackerService
{
    private readonly HttpClient _httpClient;

    public GainsTrackerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<WorkoutDto>> GetUserWorkouts()
    {
        var url = $"{BaseUrl}/gains/user/workout";
        try
        {
            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();


            return new List<WorkoutDto>(); //TODO: fix, obviously.
        }
        catch (Exception ex)
        {
            Debug.WriteLine("uh oh:");
            Debug.WriteLine(ex);
            return new List<WorkoutDto>();
        }
    }

    public async Task<List<MeasurementDto>> GetPersonalBests()
    {
        var workouts = await GetUserWorkouts();
        var personalBests = workouts.Select(workout => workout.PersonalBest ?? new MeasurementDto()).ToList();
        return personalBests;
    }
}

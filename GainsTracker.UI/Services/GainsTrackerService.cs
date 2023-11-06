using System.Diagnostics;
using GainsTracker.Common.Models.Workouts.Dto;
using static GainsTracker.Common.Constants;

namespace GainsTracker.UI.Services;

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
        string url = $"{BaseUrl}/gains/user/workout";
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            
            
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

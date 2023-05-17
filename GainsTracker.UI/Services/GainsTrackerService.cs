using System.Diagnostics;
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

    public async Task<string> GetUserWorkouts()
    {
        string url = $"{BaseUrl}/gains/user/workout";
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            return json;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("uh oh:");
            Debug.WriteLine(ex);
            return null;
        }
    }
}

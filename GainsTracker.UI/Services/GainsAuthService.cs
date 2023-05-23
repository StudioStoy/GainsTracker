using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using static GainsTracker.Common.Constants;

namespace GainsTracker.UI.Services;

public class GainsAuthService : IGainsAuthService
{
    private readonly HttpClient _httpClient;

    public GainsAuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> PingApiHealth()
    {
        const string url = $"{BaseUrl}/health";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            JsonNode? jsonString = JsonNode.Parse(await response.Content.ReadAsStringAsync());

            Debug.WriteLine(jsonString?["status"]?.ToString() == "UP"
                ? "Server successfully responded."
                : "Server responded, but not with the correct health indication.");

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> SignUp(string email, string password)
    {
        const string url = $"{BaseUrl}/authentication/register";

        try
        {
            JsonObject loginDto = new()
            {
                { "username", email },
                { "password", password },
                { "firstName", "Standard" },
                { "lastName", "User" }

                //TODO: Expand with actual name and other info of the user.
            };

            StringContent content = new(loginDto.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            ValidateResponse(response);

            //TODO: This response contains the JWT. implement the setting authorization of the returned token here.

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> Login(string email, string password)
    {
        const string url = $"{BaseUrl}/authentication/login";

        try
        {
            JsonObject loginDto = new()
            {
                { "userHandle", email },
                { "password", password }
            };
            StringContent content = new(loginDto.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            ValidateResponse(response);

            string token = await response.Content.ReadAsStringAsync();

            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    // For now a quick and dirty way :).
    private async void ValidateResponse(HttpResponseMessage response)
    {
        if (response == null) throw new ArgumentException("no response.");

        string token = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(token)) throw new ArgumentException("No valid token.");

        if (response.IsSuccessStatusCode) return;

        throw response.StatusCode switch
        {
            HttpStatusCode.NotFound => new ArgumentException("not found."),
            HttpStatusCode.BadRequest => new ArgumentException("Credentials not valid."),
            _ => new ArgumentOutOfRangeException()
        };
    }
}

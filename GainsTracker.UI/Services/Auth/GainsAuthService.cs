using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using GainsTracker.Common.Models.Auth.Dto;
using GainsTracker.UI.Services.API;
using GainsTracker.UI.Services.Auth.Interfaces;
using static GainsTracker.Common.Constants;

namespace GainsTracker.UI.Services.Auth;

public class GainsAuthService(ApiService api)
    : IGainsAuthService
{
    private const string Url = $"{BaseUrl}/auth";

    public async Task<bool> Register(string userHandle, string lastName, string email, string password)
    {
        try
        {
            RegisterRequestDto requestDto = new(userHandle, email, password, null);
            var response = await api.PostAsync($"{Url}/register", requestDto);

            ValidateResponse(response);

            // Set token in authorization state upon registration.
            var token = await response.Content.ReadAsStringAsync();
            // if (!string.IsNullOrEmpty(token))
                // await authStateProvider.SetTokenAsync(token);

            Console.WriteLine(token);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    public async Task<bool> Login2(string email, string password)
    {
        try
        {
            JsonObject loginDto = new()
            {
                { "userHandle", email },
                { "password", password },
            };
            
            var response = await api.PostAsync($"{Url}/login", loginDto);

            ValidateResponse(response);

            var token = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(token)) return false;
            
            // Set token in authorization state upon login.
            // await authStateProvider.SetTokenAsync(token);

            return true;
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
        if (response == null)
            throw new ArgumentException("No response.");

        if (response.IsSuccessStatusCode) return;

        Exception throwable = response.StatusCode switch
        {
            HttpStatusCode.NotFound => new ArgumentException("Not found."),
            HttpStatusCode.BadRequest => new ArgumentException("Credentials not valid."),
            _ => throw new ArgumentOutOfRangeException(),
        };

        if (throwable != null)
            throw throwable;

        var token = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(token))
            throw new ArgumentException("No valid token.");
    }

    public async Task<bool> Login(string username, string password)
    {
        var payload = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "username", username },
            { "password", password },
            { "client_id", "CGofh0U2vV2LF3O593BL38oBlX7GzOly" },
            { "client_secret", "secret" },
            { "audience", "https://dev-gainstracker.eu.auth0.com/api/v2/" }
        };

        var response = await api.PostAsync("https://dev-gainstracker.eu.auth0.com/oauth/token", payload);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<AuthTokenResponse>();
            // Save tokens (e.g., access_token, id_token) for later use
            // await authStateProvider.SetTokenAsync(result!.AccessToken);

        }
        else
        {
            // Handle login failure
            return false;
        }

        return false;
    }

    private class AuthTokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string IdToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }

}

using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GainsTracker.UI.Services.API;

public class ApiService(HttpClient httpClient)
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    /// --- GET with typed result ---
    public async Task<ApiResult<T>> GetAsync<T>(string requestUri)
    {
        var response = await httpClient.GetAsync(requestUri);
        return await CreateResult<T>(response);
    }

    // --- POST with optional response payload ---
    public async Task<ApiResult<T>> PostAsync<T>(string requestUri, object postData)
    {
        var content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(requestUri, content);
        return await CreateResult<T>(response);
    }

    // --- POST without expecting response body ---
    public async Task<ApiResult> PostAsync(string requestUri, object postData)
    {
        var content = new StringContent(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(requestUri, content);
        return CreateResult(response);
    }

    public async Task<ApiResult<T>> PutAsync<T>(string requestUri, object putData)
    {
        var content = new StringContent(JsonSerializer.Serialize(putData), Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync(requestUri, content);
        return await CreateResult<T>(response);
    }

    public async Task<ApiResult> DeleteAsync(string requestUri)
    {
        var response = await httpClient.DeleteAsync(requestUri);
        return CreateResult(response);
    }

    public async Task<ApiResult<T>> PatchAsync<T>(string requestUri, object? patchData = null)
    {
        StringContent? content = null;
        if (patchData != null)
            content = new StringContent(JsonSerializer.Serialize(patchData), Encoding.UTF8, "application/json");

        var response = await httpClient.PatchAsync(requestUri, content);
        return await CreateResult<T>(response);
    }

    private async Task<ApiResult<T>> CreateResult<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            return ApiResult<T>.Failure(response.StatusCode, await response.Content.ReadAsStringAsync());

        if (response.StatusCode == HttpStatusCode.NoContent)
            return ApiResult<T>.Success(default, response.StatusCode, isEmpty: true);

        var content = await response.Content.ReadAsStringAsync();
        var value = string.IsNullOrWhiteSpace(content)
            ? default
            : JsonSerializer.Deserialize<T>(content, _jsonOptions);

        return ApiResult<T>.Success(value, response.StatusCode);
    }

    private static ApiResult CreateResult(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode) return ApiResult.Failure(response.StatusCode, response.ReasonPhrase);

        return ApiResult.Success(
            response.StatusCode,
            response.StatusCode == HttpStatusCode.NoContent
        );
    }
}

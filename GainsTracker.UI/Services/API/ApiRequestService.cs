using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GainsTracker.UI.Services.API;

public class ApiService(HttpClient httpClient)
{
    public async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        return await httpClient.GetAsync(requestUri);
    }

    public async Task<HttpResponseMessage> PostAsync(string requestUri, object postData)
    {
        StringContent content = new(JsonSerializer.Serialize(postData), Encoding.UTF8, "application/json");
        return await httpClient.PostAsync(requestUri, content);
    }

    public async Task<HttpResponseMessage> PutAsync(string requestUri, object putData)
    {
        StringContent content = new(JsonSerializer.Serialize(putData), Encoding.UTF8, "application/json");
        return await httpClient.PutAsync(requestUri, content);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
    {
        return await httpClient.DeleteAsync(requestUri);
    }

    public async Task<HttpResponseMessage> PatchAsync(string requestUri, object? patchData = null)
    {
        StringContent? content = null;
        if (patchData != null)
            content = new StringContent(JsonSerializer.Serialize(patchData), Encoding.UTF8, "application/json");

        return await httpClient.PatchAsync(requestUri, content);
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        return await httpClient.SendAsync(request);
    }
}

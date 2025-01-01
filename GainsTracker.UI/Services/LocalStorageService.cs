namespace GainsTracker.UI.Services;

using Microsoft.JSInterop;
using System.Text.Json;
using System.Threading.Tasks;

public interface ILocalStorageService
{
    Task SetItem<T>(string key, T value);
    ValueTask<T?> GetItem<T>(string key);
    ValueTask<bool> ContainsKey(string key);
    Task RemoveItem(string key);
}

public class LocalStorageService(IJSRuntime jsRuntime) : ILocalStorageService
{
    public async Task SetItem<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
    }

    public async ValueTask<T?> GetItem<T>(string key)
    {
        var data = await jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        return string.IsNullOrEmpty(data) ? default : JsonSerializer.Deserialize<T>(data);
    }

    public async ValueTask<bool> ContainsKey(string key)
    {
        var item = await jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        return !string.IsNullOrEmpty(item);
    }


    public async Task RemoveItem(string key)
    {
        await jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }
}

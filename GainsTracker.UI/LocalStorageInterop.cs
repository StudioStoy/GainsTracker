using System.Text.Json;
using Microsoft.JSInterop;

namespace GainsTracker.UI;

public sealed class LocalStorageInterop
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public ValueTask Clear()
    {
        return _jsRuntime.InvokeVoidAsync("localStorage.clear");
    }

    public async ValueTask<T?> GetItem<T>(string key)
    {
        string data = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        return JsonSerializer.Deserialize<T>(data);
    }

    public ValueTask<string> Key(int index)
    {
        return _jsRuntime.InvokeAsync<string>("localStorage.key", index);
    }

    public ValueTask<bool> ContainKey(string key)
    {
        return _jsRuntime.InvokeAsync<bool>("localStorage.hasOwnProperty", key);
    }

    public ValueTask<int> Length()
    {
        return _jsRuntime.InvokeAsync<int>("eval", "localStorage.length");
    }

    public ValueTask RemoveItem(string key)
    {
        return _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }

    public async ValueTask SetItem<T>(string key, T? data)
    {
        string obj = JsonSerializer.Serialize(data);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, obj);
    }
}

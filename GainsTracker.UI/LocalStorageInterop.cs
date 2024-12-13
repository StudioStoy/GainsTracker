#region

using System.Text.Json;
using Microsoft.JSInterop;

#endregion

namespace GainsTracker.UI;

public sealed class LocalStorageInterop
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public ValueTask Clear() => _jsRuntime.InvokeVoidAsync("localStorage.clear");

    public async ValueTask<T?> GetItem<T>(string key)
    {
        var data = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        return JsonSerializer.Deserialize<T>(data);
    }

    public ValueTask<string> Key(int index) => _jsRuntime.InvokeAsync<string>("localStorage.key", index);

    public ValueTask<bool> ContainKey(string key) => _jsRuntime.InvokeAsync<bool>("localStorage.hasOwnProperty", key);

    public ValueTask<int> Length() => _jsRuntime.InvokeAsync<int>("eval", "localStorage.length");

    public ValueTask RemoveItem(string key) => _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);

    public async ValueTask SetItem<T>(string key, T? data)
    {
        var obj = JsonSerializer.Serialize(data);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, obj);
    }
}

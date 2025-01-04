using Microsoft.JSInterop;

namespace GainsTracker.UI.TransitionableRoute.Interop;

public class JsInterop(IJSRuntime jsRuntime) : IJsInterop, IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
        "import", "./_content/GainsTracker.UI.Transitions/jsInterop.js").AsTask());

    public async ValueTask DisposeAsync()
    {
        if (!_moduleTask.IsValueCreated) return;

        var module = await _moduleTask.Value;
        await module.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public async Task Init<T>(DotNetObjectReference<T> instance, bool isActive) where T : class
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("init", instance, isActive);
    }
}

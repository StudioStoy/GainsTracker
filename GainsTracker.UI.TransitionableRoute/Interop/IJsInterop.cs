using Microsoft.JSInterop;

namespace GainsTracker.UI.TransitionableRoute.Interop;

public interface IJsInterop
{
    Task Init<T>(DotNetObjectReference<T> instance, bool isActive) where T : class;
}

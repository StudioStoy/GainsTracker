using GainsTracker.UI.TransitionableRoute.Interop;
using GainsTracker.UI.TransitionableRoute.Layout;
using GainsTracker.UI.TransitionableRoute.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GainsTracker.UI.TransitionableRoute.Route;

/// <summary>
///     <para>
///         The TransitionableRoutePrimary and <see cref="TransitionableRouteSecondary" />
///         should always be used together. They are for keeping the component renders alive during transitions.
///         The TransitionableRoutePrimary is used within a common Layout and is the first recipient of the found
///         context routeData.
///     </para>
///     <para>
///         These components should have a single <see cref="TransitionableRouteView" /> child component with a
///         DefaultView that implements or inherits the <see cref="ITransitionableLayoutComponent" />.
///     </para>
///     <para>
///         The <see cref="Navigate(bool)" /> method is `JSInvokable` and triggered from
///         JSInterop when browser page location changes.  This method was chosen over the
///         Blazor <see cref="NavigationManager.LocationChanged" />, due to the JSInterop being able to also
///         respond to browser navigation forward and back, which is not currently supported by the Blazor method.
///     </para>
///     <para>
///         Add a <see cref="TransitionableRouteView" /> as a child of this
///         with the DefaultView that implements or inherits the
///         <see cref="ITransitionableLayoutComponent" />
///     </para>
/// </summary>
public partial class TransitionableRoutePrimary : ComponentBase
{
    private const bool InvokesStateChanged = true;
    private bool _isActive = true;

    private JsInterop? _jsInterop;
    private RouteData? _lastRouteData;

    [Inject] public required IJSRuntime JsRuntime { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RouteData? RouteData { get; set; }
    [Parameter] public Transition? Transition { get; set; }
    [Parameter] public bool ForgetStateOnTransition { get; set; }
    [Parameter] public int TransitionDurationMilliseconds { get; set; } = 1000;

    internal void MakeSecondary() => _isActive = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) await HandleFirstRender();

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task HandleFirstRender()
    {
        _jsInterop ??= new JsInterop(JsRuntime);
        await _jsInterop.Init(DotNetObjectReference.Create(this), _isActive);
        await Navigate(false, true);
    }

    /// <summary>
    ///     The navigation that's called by the JsInterop during runtime, when page changes occur (`forward`, `back`).
    ///     See <see cref="wwwroot/jsInterop.js" />
    /// </summary>
    /// <param name="backwards">Flag to check if the page changes to the previous page.</param>
    [JSInvokable]
    public async Task Navigate(bool backwards) => await Navigate(backwards, false);

    private async Task Navigate(bool backwards, bool firstRender)
    {
        var routeDataToUse = _isActive ? RouteData : _lastRouteData;
        var switchedRouteData = (_isActive ? _lastRouteData : RouteData) ?? RouteData;

        Transition = Transition.Create(routeDataToUse, switchedRouteData, _isActive, backwards, firstRender);

        if (InvokesStateChanged) StateHasChanged();

        var canResetStateOnTransitionOut = ForgetStateOnTransition && !_isActive;

        await Task.Yield();

        _isActive = !_isActive;
        _lastRouteData = RouteData;

        if (!canResetStateOnTransitionOut) return;

        Transition = Transition.Create(
            null,
            null,
            Transition.IntoView,
            Transition.Backwards,
            Transition.FirstRender
        );
        
        await Task.Delay(TransitionDurationMilliseconds);
        
        if (InvokesStateChanged) StateHasChanged();
    }
}

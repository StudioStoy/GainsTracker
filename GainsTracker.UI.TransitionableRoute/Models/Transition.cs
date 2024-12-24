using Microsoft.AspNetCore.Components;

namespace GainsTracker.UI.TransitionableRoute.Models;

public class Transition
{
    private Transition(RouteData? routeData, RouteData? switchedRouteData, bool intoView, bool backwards,
        bool firstRender)
    {
        RouteData = routeData;
        SwitchedRouteData = switchedRouteData;
        IntoView = intoView;
        Backwards = backwards;
        FirstRender = firstRender;
    }

    public RouteData? RouteData { get; }
    public RouteData? SwitchedRouteData { get; }
    public bool IntoView { get; }
    public bool Backwards { get; }
    public bool FirstRender { get; }

    public static Transition Create(RouteData? routeData, RouteData? switchedRouteData, bool intoView, bool backwards,
        bool firstRender) =>
        new(routeData, switchedRouteData, intoView, backwards, firstRender);
}

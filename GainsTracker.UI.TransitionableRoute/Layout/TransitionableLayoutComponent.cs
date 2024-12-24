using GainsTracker.UI.TransitionableRoute.Models;
using Microsoft.AspNetCore.Components;

namespace GainsTracker.UI.TransitionableRoute.Layout;

public class TransitionableLayoutComponent : LayoutComponentBase, ITransitionableLayoutComponent
{
    [CascadingParameter] public required Transition Transition { get; set; }
    
    protected (Type fromType, Type toType) TransitionType =>
        (Transition.IntoView ? Transition.SwitchedRouteData?.PageType : Transition.RouteData?.PageType, 
            Transition.IntoView ? Transition.RouteData?.PageType : Transition.SwitchedRouteData?.PageType)!;
}

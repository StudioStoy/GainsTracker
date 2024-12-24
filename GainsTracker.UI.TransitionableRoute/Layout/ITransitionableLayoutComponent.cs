using GainsTracker.UI.TransitionableRoute.Models;
using Microsoft.AspNetCore.Components;

namespace GainsTracker.UI.TransitionableRoute.Layout;

/// <summary>
///     This interface can be implemented to get access
///     to the <see cref="Transition" /> object. With this, the markup can be adjusted to invoke transition behaviour.
/// </summary>
public interface ITransitionableLayoutComponent
{
    /// <summary>
    ///     Contains information on the transition behaviour to adjust the view.
    /// </summary>
    [CascadingParameter] Transition Transition { get; set; }
}

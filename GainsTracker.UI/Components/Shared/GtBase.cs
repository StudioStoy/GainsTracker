using Microsoft.AspNetCore.Components;

namespace GainsTracker.UI.Components.Shared;

public abstract class GtBase : ComponentBase
{
    /// <summary>
    /// Additional CSS classes for the component.
    /// </summary>
    [Parameter] public string? Class { get; set; }
}

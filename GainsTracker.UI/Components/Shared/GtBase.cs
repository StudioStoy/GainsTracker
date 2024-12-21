using Microsoft.AspNetCore.Components;

namespace GainsTracker.UI.Components.Shared;

public abstract class GtBase : ComponentBase
{
    /// <summary>
    /// Additional CSS classes for the component.
    /// </summary>
    [Parameter] public string? Class { get; set; }

    /// <summary>
    /// The group which this component is registered to, if in group.
    /// </summary>
    /// <remarks>Optional.</remarks>
    [CascadingParameter] private GtGroup.GtGroup? ParentGtGroup { get; set; }

    protected override void OnInitialized()
    {
        ParentGtGroup?.RegisterChild(this);
        base.OnInitialized();
    }

    /// <summary>
    /// Validates the input of the component if required. 
    /// </summary>
    /// <remarks>Defaults to true if it's a component without inputs.</remarks>
    /// <returns></returns>
    public virtual bool Validate() => true;
}

#region

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace GainsTracker.WebAPI;

/// <summary>
///     Extended controller class to add commonly used controller functionality.
/// </summary>
public abstract class ExtendedControllerBase : ControllerBase
{
    private const string AnonymousUserName = "Anonymous";
    protected string CurrentUsername => User.FindFirstValue(ClaimTypes.Name) ?? AnonymousUserName;
}

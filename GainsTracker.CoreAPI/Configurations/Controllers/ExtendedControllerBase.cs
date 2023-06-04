using System.Security.Claims;
using GainsTracker.Common;
using Microsoft.AspNetCore.Mvc;

namespace GainsTracker.CoreAPI.Configurations.Controllers;

/// <summary>
/// Extended controller class to add commonly used controller functionality. 
/// </summary>
public abstract class ExtendedControllerBase : ControllerBase
{
    protected string CurrentUsername => User.FindFirstValue(ClaimTypes.Name) ?? Constants.AnonymousUserName;
}

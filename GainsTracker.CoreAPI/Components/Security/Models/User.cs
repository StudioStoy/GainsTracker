using GainsTracker.CoreAPI.Components.Gains.Models;
using Microsoft.AspNetCore.Identity;

namespace GainsTracker.CoreAPI.Components.Security.Models;

public class User : IdentityUser
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public GainsAccount GainsAccount { get; set; }
}

using GainsTracker.Core.Gains.Models;
using Microsoft.AspNetCore.Identity;

namespace GainsTracker.Core.Security.Models;

public sealed class User : IdentityUser
{
    public User() {}
    
    public User(string userHandle, string displayName = "")
    {
        UserName = userHandle;
        GainsAccount = new GainsAccount(userHandle, displayName);
    }

    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public Guid GainsAccountId { get; set; }
    public GainsAccount? GainsAccount { get; set; }
}

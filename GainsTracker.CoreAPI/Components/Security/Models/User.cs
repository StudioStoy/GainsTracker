using GainsTracker.CoreAPI.Components.Workouts.Models;
using Microsoft.AspNetCore.Identity;

namespace GainsTracker.CoreAPI.Components.Security.Models;

public sealed class User : IdentityUser
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public string GainsAccountId { get; set; }
    public GainsAccount? GainsAccount { get; set; }

    private User() {}
    
    public User(string userHandle)
    {
        UserName = userHandle;
        GainsAccount = new GainsAccount(userHandle);
        GainsAccountId = GainsAccount.Id;
    }
}

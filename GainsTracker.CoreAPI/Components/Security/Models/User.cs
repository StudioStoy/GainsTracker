using GainsTracker.CoreAPI.Components.Workouts.Models;
using Microsoft.AspNetCore.Identity;

namespace GainsTracker.CoreAPI.Components.Security.Models;

public sealed class User : IdentityUser
{
    private User()
    {
    }

    public User(string userHandle, string displayName = "")
    {
        UserName = userHandle;
        GainsAccount = new GainsAccount(userHandle)
        {
            DisplayName = displayName
        };

        // Id stuff for EF.
        GainsAccountId = GainsAccount.Id;
        GainsAccount.UserId = Id;
    }

    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public string GainsAccountId { get; set; }
    public GainsAccount GainsAccount { get; set; }
}

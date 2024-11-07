using GainsTracker.Core.Components.Workouts.Models;

namespace GainsTracker.Core.Components.Security.Models;

public sealed class User : IdentityUser
{
    // Used for EF Core
    private User()
    {
    }

    public User(string userHandle, string displayName = "")
    {
        UserName = userHandle;
        GainsAccount = new GainsAccount(userHandle, displayName);

        // Id stuff for EF.
        GainsAccountId = GainsAccount.Id;
        GainsAccount.UserId = Id;
    }

    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public string GainsAccountId { get; set; } = "";
    public GainsAccount? GainsAccount { get; set; }
}

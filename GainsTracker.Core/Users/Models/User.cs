using GainsTracker.Common.Models.UserDtos;
using GainsTracker.Core.Gains.Models;

namespace GainsTracker.Core.Users.Models;

public sealed class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string AuthIdentifier { get; init; } = null!;
    public Guid GainsAccountId { get; init; }
    public GainsAccount GainsAccount { get; init; } = null!;

    public UserRole Role { get; init; }
    public string Email { get; init; } = null!;
    public string Handle { get; init; } = null!;

    public User() {}
    
    public User(string authIdentifier, UserRole role, string email, string userHandle)
    {
        AuthIdentifier = authIdentifier;
        Role = role;
        Email = email;
        Handle = userHandle;
        GainsAccount = new GainsAccount(userHandle);
        GainsAccountId = GainsAccount.Id;
    }
}

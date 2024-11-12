using Microsoft.AspNetCore.Identity;

namespace GainsTracker.Data.Gains.Entities;

public class UserEntity : IdentityUser
{
    public UserEntity() {}

    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public Guid GainsAccountId { get; set; }
    public GainsAccountEntity? GainsAccount { get; set; }
}

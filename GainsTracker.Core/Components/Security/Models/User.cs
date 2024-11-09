﻿using GainsTracker.Core.Components.Workouts.Models;
using Microsoft.AspNetCore.Identity;

namespace GainsTracker.Core.Components.Security.Models;

public sealed class User : IdentityUser
{
    public User(string userHandle, string displayName = "")
    {
        UserName = userHandle;
        GainsAccount = new GainsAccount(userHandle, displayName);
    }

    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public Guid GainsAccountId { get; set; }
    public GainsAccount? GainsAccount { get; set; }
}
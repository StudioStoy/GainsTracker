using GainsTracker.Core.Auth.Models;
using GainsTracker.Core.Gains.Models;
using GainsTracker.Data.Friends;
using GainsTracker.Data.Gains.Entities;
using GainsTracker.Data.HealthMetrics;
using GainsTracker.Data.UserProfiles;
using GainsTracker.Data.Workouts;

namespace GainsTracker.Data.Gains;

public static class GainsAccountExtensions
{
    // GainsAccount
    public static GainsAccount ToModel(this GainsAccountEntity entity, HashSet<object>? processed = null)
    {
        processed ??= [];

        if (!processed.Add(entity))
            return null!; // Handle cyclic references

        var model = new GainsAccount(entity.UserHandle)
        {
            Id = entity.Id,
            UserProfileId = entity.UserProfileId,
            UserProfile = entity.UserProfile?.ToModel(),
            Workouts = entity.Workouts.Select(w => w.ToModel()).ToList(),
            Metrics = entity.Metrics.Select(m => m.ToModel()).ToList(),
            Friends = entity.Friends.Select(f => f.ToModel()).ToList(),
            UserHandle = entity.UserHandle,
            ReceivedFriendRequests = entity.ReceivedFriendRequests.Select(r => r.ToModel(processed)).ToList(),
            SentFriendRequests = entity.SentFriendRequests.Select(r => r.ToModel(processed)).ToList(),
        };

        return model;
    }

    public static GainsAccountEntity ToEntity(this GainsAccount model, HashSet<object>? processed = null)
    {
        processed ??= [];

        if (!processed.Add(model))
            return null!; // Handle cyclic references

        GainsAccountEntity entity;
        if (model.UserProfile == null)
            entity = new GainsAccountEntity
            {
                Id = model.Id,
                UserProfileId = model.UserProfileId,
                UserHandle = model.UserHandle,
                Workouts = model.Workouts.Select(w => w.ToEntity()).ToList(),
                Metrics = model.Metrics.Select(m => m.ToEntity()).ToList(),
                Friends = model.Friends.Select(f => f.ToEntity()).ToList(),
                ReceivedFriendRequests = model.ReceivedFriendRequests.Select(fr => fr.ToEntity(processed)).ToList(),
                SentFriendRequests = model.SentFriendRequests.Select(fr => fr.ToEntity(processed)).ToList(),
            };
        else
            entity = new GainsAccountEntity
            {
                Id = model.Id,
                UserProfileId = model.UserProfileId,
                UserHandle = model.UserHandle,
                UserProfile = model.UserProfile.ToEntity(),
                Workouts = model.Workouts.Select(w => w.ToEntity()).ToList(),
                Metrics = model.Metrics.Select(m => m.ToEntity()).ToList(),
                Friends = model.Friends.Select(f => f.ToEntity()).ToList(),
                ReceivedFriendRequests = model.ReceivedFriendRequests.Select(fr => fr.ToEntity(processed)).ToList(),
                SentFriendRequests = model.SentFriendRequests.Select(fr => fr.ToEntity(processed)).ToList(),
            };

        return entity;
    }

    // User
    public static User ToModel(this UserEntity entity, HashSet<object>? processed = null)
    {
        processed ??= [];

        if (!processed.Add(entity))
            return null!; // Handle cyclic references

        return new User
        {
            Id = entity.Id,
            GainsAccount = entity.GainsAccount?.ToModel(processed),
            GainsAccountId = entity.GainsAccountId,
            Email = entity.Email,
            UserName = entity.UserName,
        };
    }

    public static UserEntity ToEntity(this User model, HashSet<object>? processed = null)
    {
        processed ??= [];

        if (!processed.Add(model))
            return null!; // Handle cyclic references

        return new UserEntity
        {
            Id = model.Id,
            GainsAccount = model.GainsAccount?.ToEntity(processed),
            GainsAccountId = model.GainsAccount?.Id ?? Guid.Empty,
            Email = model.Email,
            UserName = model.UserName,
        };
    }
}

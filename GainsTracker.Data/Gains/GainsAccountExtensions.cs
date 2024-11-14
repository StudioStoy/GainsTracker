using GainsTracker.Core.Gains.Models;
using GainsTracker.Core.Security.Models;
using GainsTracker.Core.Workouts.Models;
using GainsTracker.Data.Friends;
using GainsTracker.Data.Gains.Entities;
using GainsTracker.Data.HealthMetrics;
using GainsTracker.Data.UserProfiles;
using GainsTracker.Data.Workouts;

namespace GainsTracker.Data.Gains;

public static class GainsAccountExtensions
{
    // GainsAccount
    public static GainsAccount MapToModel(this GainsAccountEntity entity)
    {
        // Map UserProfile, Workouts, Metrics, Friends, etc., using the existing MapToModel extensions
        var model = new GainsAccount(entity.UserHandle)
        {
            Id = entity.Id,
            UserProfile = entity.UserProfile.MapToModel(),
            Workouts = entity.Workouts.Select(w => w.MapToModel()).ToList(),
            Metrics = entity.Metrics.Select(m => m.MapToModel()).ToList(), 
            Friends = entity.Friends.Select(f => f.MapToModel()).ToList(), 
            ReceivedFriendRequests = entity.ReceivedFriendRequests.Select(fr => fr.MapToModel()).ToList(),
            SentFriendRequests = entity.SentFriendRequests.Select(fr => fr.MapToModel()).ToList() 
        };

        return model;
    }

    public static GainsAccountEntity MapToEntity(this GainsAccount model)
    {
        var entity = new GainsAccountEntity
        {
            Id = model.Id,
            UserProfileId = model.UserProfileId,
            UserHandle = model.UserHandle,
            UserProfile = model.UserProfile.MapToEntity(), 
            Workouts = model.Workouts.Select(w => w.MapToEntity()).ToList(), 
            Metrics = model.Metrics.Select(m => m.MapToEntity()).ToList(),
            Friends = model.Friends.Select(f => f.MapToEntity()).ToList(), 
            ReceivedFriendRequests = model.ReceivedFriendRequests.Select(fr => fr.MapToEntity()).ToList(), 
            SentFriendRequests = model.SentFriendRequests.Select(fr => fr.MapToEntity()).ToList()
        };

        return entity;
    }
    
    // User
    public static User MapToModel(this UserEntity entity)
    {
        return new User
        {
            Id = entity.Id,
            GainsAccount = entity.GainsAccount?.MapToModel(),
            GainsAccountId = entity.GainsAccountId,
            Email = entity.Email,
            UserName = entity.UserName
        };
    }

    public static UserEntity MapToEntity(this User model)
    {
        return new UserEntity
        {
            Id = model.Id,
            GainsAccount = model.GainsAccount?.MapToEntity(),
            GainsAccountId = model.GainsAccount?.Id ?? Guid.Empty,
            Email = model.Email,
            UserName = model.UserName
        };
    }
}
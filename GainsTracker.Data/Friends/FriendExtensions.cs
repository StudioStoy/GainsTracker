using System.Linq.Expressions;
using GainsTracker.Core.Friends.Models;
using GainsTracker.Data.Friends.Entities;
using GainsTracker.Data.Gains;

namespace GainsTracker.Data.Friends;

public static class FriendExtensions
{
    // Friend
    public static Friend ToModel(this FriendEntity entity)
    {
        return new Friend(entity.FriendName, entity.FriendHandle, entity.GainsAccountId, entity.FriendsSince)
        {
            Id = entity.Id,
        };
    }

    public static FriendEntity ToEntity(this Friend model)
    {
        return new FriendEntity
        {
            Id = model.Id,
            FriendName = model.FriendName,
            FriendHandle = model.FriendHandle,
            GainsAccountId = model.GainsAccountId,
            FriendsSince = model.FriendsSince,
        };
    }

    // FriendRequest
    public static FriendRequest ToModel(this FriendRequestEntity entity, HashSet<object>? processed = null)
    {
        processed ??= [];

        if (!processed.Add(entity))
            return null!; // Handle cyclic references

        return new FriendRequest(entity.Requester.ToModel(), entity.Recipient.ToModel())
        {
            Id = entity.Id,
            Status = entity.Status,
            RequestTime = entity.RequestTime,
        };
    }

    public static FriendRequestEntity ToEntity(this FriendRequest model, HashSet<object>? processed = null)
    {
        processed ??= [];

        // Avoid infinite loop by checking if this object is already processed
        if (!processed.Add(model))
            return null!; // Return a placeholder or handle appropriately

        return new FriendRequestEntity
        {
            Id = model.Id,
            RecipientId = model.RecipientId,
            Recipient = model.Recipient.ToEntity(processed),
            RequesterId = model.RequesterId,
            Requester = model.Requester.ToEntity(processed),
            Status = model.Status,
            RequestTime = model.RequestTime,
        };
    }

    public static Expression<Func<FriendEntity, Friend>> ToModelExpression()
    {
        return entity => new Friend(entity.FriendName, entity.FriendHandle, entity.GainsAccountId, entity.FriendsSince)
        {
            Id = entity.Id,
        };
    }
}

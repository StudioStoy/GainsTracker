using GainsTracker.Core.Components.Friends;
using GainsTracker.Core.Components.Friends.Models;
using GainsTracker.Data.Gains;

namespace GainsTracker.Data.Friends;

public static class FriendExtensions
{
    // Friend
    public static Friend MapToModel(this FriendEntity entity)
    {
        return new Friend(entity.FriendName, entity.FriendHandle, entity.GainsAccountId, entity.FriendsSince)
        {
            Id = entity.Id,
        };
    }
    
    public static FriendEntity MapToEntity(this Friend model)
    {
        return new FriendEntity
        {
            Id = model.Id,
            FriendName = model.FriendName,
            FriendHandle = model.FriendHandle,
            GainsAccountId = model.GainsAccountId,
            FriendsSince = model.FriendsSince
        };
    }
    
    // FriendRequest
    public static FriendRequest MapToModel(this FriendRequestEntity entity)
    {
        return new FriendRequest(entity.Requester.MapToModel(), entity.Recipient.MapToModel())
        {
            Id = entity.Id,
            Status = entity.Status,
            RequestTime = entity.RequestTime
        };
    }
    
    public static FriendRequestEntity MapToEntity(this FriendRequest model)
    {
        return new FriendRequestEntity
        {
            Id = model.Id,
            RecipientId = model.RecipientId,
            Recipient = model.Recipient.MapToEntity(),
            RequesterId = model.RequesterId,
            Requester = model.Requester.MapToEntity(),
            Status = model.Status,
            RequestTime = model.RequestTime,
        };
    }
}

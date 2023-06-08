using GainsTracker.Common.Models.Friends.Dto;
using GainsTracker.CoreAPI.Components.Friends.Models;

namespace GainsTracker.CoreAPI.Components.Friends;

public static class FriendExtensions
{
    public static FriendRequestDto ToDto(this FriendRequest request)
    {
        string byName = request.Requester.DisplayName;
        string toName = request.Recipient.DisplayName;
        byName = !string.IsNullOrEmpty(byName)
            ? byName + $" (@{request.Requester.UserHandle})"
            : request.Requester.UserHandle;
        toName = !string.IsNullOrEmpty(toName)
            ? toName + $" (@{request.Recipient.UserHandle})"
            : request.Recipient.UserHandle;

        return new FriendRequestDto
        (
            request.Id,
            requesterId: request.RequesterId,
            recipientId: request.RecipientId,
            requesterName: byName,
            recipientName: toName,
            requestTime: request.RequestTime.ToLongDateString(),
            status: request.Status.ToString()
        );
    }
}

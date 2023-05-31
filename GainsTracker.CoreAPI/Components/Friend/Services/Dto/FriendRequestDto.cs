using GainsTracker.CoreAPI.Components.Friend.Models;

namespace GainsTracker.CoreAPI.Components.Friend.Services.Dto;

public class FriendRequestDto
{
    private FriendRequestDto(string id, string requestedByName, string requestedToName, string requestTime, string status, string requestedById, string requestedToId)
    {
        Id = id;
        RequestedById = requestedById;
        RequestedToId = requestedToId;
        RequestedByName = requestedByName;
        RequestedToName = requestedToName;
        RequestTime = requestTime;
        Status = status;
    }

    public string Id { get; set; }

    public string RequestedById { get; set; }
    public string RequestedToId { get; set; }

    public string RequestedByName { get; set; }
    public string RequestedToName { get; set; }

    public string RequestTime { get; set; }
    public string Status { get; set; }

    public static FriendRequestDto FromFriendRequest(FriendRequest request)
    {
        string byName = request.RequestedBy.DisplayName;
        string toName = request.RequestedTo.DisplayName;
        byName = !string.IsNullOrEmpty(byName)
            ? byName + $" (@{request.RequestedBy.UserHandle})"
            : request.RequestedBy.UserHandle;
        toName = !string.IsNullOrEmpty(toName)
            ? toName + $" (@{request.RequestedTo.UserHandle})"
            : request.RequestedTo.UserHandle;

        return new FriendRequestDto
        (
            request.Id,
            requestedById: request.RequestedById,
            requestedToId: request.RequestedToId,
            requestedByName: byName,
            requestedToName: toName,
            requestTime: request.RequestTime.ToLongDateString(),
            status: request.Status.ToString()
        );
    }
}

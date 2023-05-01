using GainsTrackerAPI.Gains.Models.Friends;

namespace GainsTrackerAPI.Gains.Services.Dto;

public class FriendRequestDto
{
    private FriendRequestDto(string requestedByName, string requestedToName, string requestTime, string status, string requestedById, string requestedToId)
    {
        RequestedById = requestedById;
        RequestedToId = requestedToId;
        RequestedByName = requestedByName;
        RequestedToName = requestedToName;
        RequestTime = requestTime;
        Status = status;
    }

    public string RequestedById { get; set; }
    public string RequestedToId { get; set; }

    public string RequestedByName { get; set; }
    public string RequestedToName { get; set; }

    public string RequestTime { get; set; }
    public string Status { get; set; }

    public static FriendRequestDto FromFriendRequest(FriendRequest request)
    {
        FriendRequest egg = request;

        return new FriendRequestDto
        (
            requestedById: request.RequestedById,
            requestedToId: request.RequestedToId,
            requestedByName: request.RequestedBy.Username,
            requestedToName: request.RequestedTo.Username,
            requestTime: request.RequestTime.ToLongDateString(),
            status: request.Status.ToString()
        );
    }
}

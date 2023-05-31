namespace GainsTracker.CoreAPI.Components.Friend.Models.Exceptions;

public class FriendRequestAlreadySentException : Exception
{
    public FriendRequestAlreadySentException(string message = "") : base(message)
    {
    }
}

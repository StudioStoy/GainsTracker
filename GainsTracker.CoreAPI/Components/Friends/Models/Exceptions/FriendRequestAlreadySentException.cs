namespace GainsTracker.CoreAPI.Components.Friends.Models.Exceptions;

public class FriendRequestAlreadySentException : Exception
{
    public FriendRequestAlreadySentException(string message = "") : base(message)
    {
    }
}

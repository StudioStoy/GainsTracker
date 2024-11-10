namespace GainsTracker.Core.Friends.Exceptions;

public class FriendRequestAlreadySentException : Exception
{
    public FriendRequestAlreadySentException(string message = "") : base(message)
    {
    }
}

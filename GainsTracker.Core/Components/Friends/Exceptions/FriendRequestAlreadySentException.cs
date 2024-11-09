namespace GainsTracker.Core.Components.Friends.Exceptions;

public class FriendRequestAlreadySentException : Exception
{
    public FriendRequestAlreadySentException(string message = "") : base(message)
    {
    }
}

namespace GainsTracker.Core.Components.Friends.Models.Exceptions;

public class AlreadyFriendsException : Exception
{
    public AlreadyFriendsException(string message = "") : base(message)
    {
    }
}

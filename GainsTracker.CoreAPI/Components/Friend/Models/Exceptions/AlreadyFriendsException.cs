namespace GainsTracker.CoreAPI.Components.Friend.Models.Exceptions;

public class AlreadyFriendsException : Exception
{
    public AlreadyFriendsException(string message = "") : base(message)
    {
    }
}

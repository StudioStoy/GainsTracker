namespace GainsTrackerAPI.Gains.Models.Exceptions;

public class AlreadyFriendsException : Exception
{
    public AlreadyFriendsException(string message = "") : base(message)
    {
    }
}

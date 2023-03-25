namespace GainsTrackerAPI.ExceptionConfigurations.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message)
    {
    }
}

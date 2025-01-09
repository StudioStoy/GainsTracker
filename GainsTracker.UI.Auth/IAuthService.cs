namespace GainsTracker.UI.Auth;

public interface IAuthService
{
    Task Login();
    Task Logout();
}


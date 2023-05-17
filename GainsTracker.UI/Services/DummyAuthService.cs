namespace GainsTracker.UI.Services;

public class DummyAuthService : IGainsAuthService
{
    public Task<bool> PingApiHealth()
    {
        return Task.Run(() => true);
    }

    public Task<bool> SignUp(string email, string password)
    {
        return Task.Run(() => true);
    }

    public Task<bool> Login(string email, string password)
    {
        return Task.Run(() => true);
    }
}

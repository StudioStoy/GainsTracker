namespace GainsTracker.UI.Services.Auth.Interfaces;

public interface IGainsAuthService
{
    public Task<bool> PingApiHealth();
    public Task<bool> SignUp(string email, string password);
    public Task<bool> Login(string email, string password);
}

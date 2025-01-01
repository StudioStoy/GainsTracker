using System.Threading.Tasks;

namespace GainsTracker.UI.Services.Auth.Interfaces;

public interface IGainsAuthService
{
    public Task<bool> Register(string userHandle, string lastName, string email, string password);
    public Task<bool> Login(string email, string password);
}

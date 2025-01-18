using GainsTracker.Common.Models.UserDtos;

namespace GainsTracker.Core.Users.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserOrCreate(string authIdentifier, UserRole role, string email, string userHandle);
}

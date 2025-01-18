using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.UserDtos;
using GainsTracker.Core.Gains.Interfaces.Services;
using GainsTracker.Core.Users.Interfaces;
using GainsTracker.Core.Users.Interfaces.Repositories;
using GainsTracker.Core.Users.Models;

namespace GainsTracker.Core.Users.Services;

public class UserService(IUserRepository userRepository, IGainsService gainsService) : IUserService
{
    public async Task<UserDto> GetUserByAuthId(string authIdentifier)
    {
        var foundUser = await userRepository.FindSingleAsync(u => u.AuthIdentifier == authIdentifier)
                        ?? throw new NotFoundException("User with that auth id not found");

        return foundUser.ToDto();
    }

    public async Task<UserDto> GetUserOrCreate(string authIdentifier, UserRole role, string email, string userHandle)
    {
        var foundUser = await userRepository.FindSingleAsync(user => user.AuthIdentifier == authIdentifier);
        if (foundUser == null)
            return await CreateUser(authIdentifier, role, email, userHandle);

        return foundUser.ToDto();
    }

    private async Task<UserDto> CreateUser(string authIdentifier, UserRole role, string email, string userHandle)
    {
        if (string.IsNullOrEmpty(authIdentifier))
            throw new BadRequestException("Can't create a user without an Auth0 identifier");

        var newUser = new User(authIdentifier, role, email, userHandle);
        // await gainsService.SaveGainsAccountForUser(newUser.GainsAccount);
        await userRepository.AddAsync(newUser);
        
        return newUser.ToDto();
    }
}

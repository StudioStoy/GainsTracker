using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Core.Components.UserProfiles.Models;
using GainsTracker.Core.Components.Workouts.Models.Measurements;

namespace GainsTracker.Core.Components.UserProfiles.Interfaces.Repositories;

public interface IUserProfileBigBrain : IBigBrain<UserProfile>
    {
        Task UpdateUserProfileByUserHandle(string userHandle, UpdateUserProfileDto userProfileDto);
        Task<List<Measurement>> GetPinnedPBs(string userHandle);
        Task AddAndRemovePBs(string userHandle, UpdatePinnedPBsDto pinnedPBsDto);
        Task<UserProfile> GetUserProfileByUserHandle(string userHandle);
    }

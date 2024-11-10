using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Core.UserProfiles.Models;
using GainsTracker.Core.Workouts.Models.Measurements;

namespace GainsTracker.Core.UserProfiles.Interfaces.Repositories;

public interface IUserProfileBigBrain : IBaseBrain
    {
        Task UpdateUserProfileByUserHandle(string userHandle, UpdateUserProfileDto userProfileDto);
        Task<List<Measurement>> GetPinnedPBs(string userHandle);
        Task AddAndRemovePBs(string userHandle, UpdatePinnedPBsDto pinnedPBsDto);
        Task<UserProfile> GetUserProfileByUserHandle(string userHandle);
    }

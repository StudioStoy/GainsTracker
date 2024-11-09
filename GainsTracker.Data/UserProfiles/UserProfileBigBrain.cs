using System.Drawing;
using System.Linq.Expressions;
using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Core.Components.UserProfiles.Interfaces.Repositories;
using GainsTracker.Core.Components.UserProfiles.Models;
using GainsTracker.Core.Components.Workouts.Models.Measurements;
using GainsTracker.Data.Shared;
using GainsTracker.Data.Workouts;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.UserProfiles;

public delegate Expression<Func<T, object>> PropertyToInclude<T>();

public class UserProfileBigBrain(GainsDbContext context) : BigBrain<UserProfile>(context), IUserProfileBigBrain
{
    private readonly GainsDbContext _context = context;
    
    public async Task UpdateUserProfileByUserHandle(string userHandle, UpdateUserProfileDto userProfileDto)
    {
        var current = await GetUserProfileByUserHandle(userHandle);
        current.Description = userProfileDto.Description ?? current.Description;
        current.DisplayName = userProfileDto.DisplayName ?? current.DisplayName;
        current.Icon.Url = userProfileDto.IconUrl ?? current.Icon.Url;

        if (userProfileDto.IconColorHex != null)
            current.Icon.PictureColor = ColorTranslator.FromHtml(userProfileDto.IconColorHex).ToArgb();

        await SaveContext();
    }

    public async Task<List<Measurement>> GetPinnedPBs(string userHandle)
    {
        var profileEntity = await GetUserProfileByUserHandle(userHandle, () => up => up.PinnedPBs);
        return profileEntity.PinnedPBs
            .Select(pb => pb.MapToModel())
            .ToList();
    }

    public async Task AddAndRemovePBs(string userHandle, UpdatePinnedPBsDto pinnedPBsDto)
    {
        var userProfileEntity = await GetUserProfileByUserHandle(userHandle, () => up => up.PinnedPBs);
        List<MeasurementEntity> toAdd = [];
        List<MeasurementEntity> toRemove = [];

        pinnedPBsDto.AddPBs.ForEach(pb =>
        {
            var measurement = _context.Measurements.FirstOrDefault(measurement => measurement.Id == pb);
            
            if (measurement == null)
                throw new NotFoundException($"Measurement with id {pb} was not found.");

            toAdd.Add(measurement);
        });

        pinnedPBsDto.RemovePBs.ForEach(pb =>
        {
            var measurement = _context.Measurements.FirstOrDefault(measurement => measurement.Id == pb);
            if (measurement == null)
                throw new NotFoundException($"Measurement with id {pb} was not found.");

            toRemove.Add(measurement);
        });

        userProfileEntity.PinnedPBs.AddRange(toAdd);
        userProfileEntity.PinnedPBs = userProfileEntity.PinnedPBs.Except(toRemove).ToList();

        await SaveContext();
    }

    public async Task<UserProfile> GetUserProfileByUserHandle(string userHandle)
    {
        Guid gainsId = await GetGainsIdByUsername(userHandle);
        var userProfile = await _context.UserProfiles
            .Include(up => up.Icon)
            .FirstOrDefaultAsync(e => e.GainsAccountId == gainsId);

        if (userProfile == null)
            throw new NotFoundException($"UserProfile for user with id {gainsId} was not found.");
        
        return userProfile.MapToModel();
    }

    private async Task<UserProfileEntity> GetUserProfileByUserHandle(string userHandle, params PropertyToInclude<UserProfileEntity>[] properties)
    {
        Guid gainsId = await GetGainsIdByUsername(userHandle);

        // If no include expressions are provided, set a default one for the Icon.
        var includes = properties.Length != 0
            ? properties 
            : [() => up => up.Icon];

        IQueryable<UserProfileEntity> query = _context.UserProfiles.AsQueryable();

        foreach (var property in includes)
        {
            // Use the property expression to include it in the query.
            query = query.Include(property());
        }

        var userProfile = await query.FirstOrDefaultAsync(e => e.GainsAccountId == gainsId);

        if (userProfile == null)
            throw new NotFoundException($"User profile for gains account {gainsId} was not found.");
        
        return userProfile;
    }
}

#region

using System.Drawing;
using System.Linq.Expressions;
using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Core.UserProfiles.Interfaces.Repositories;
using GainsTracker.Core.UserProfiles.Models;
using GainsTracker.Core.Workouts.Models.Measurements;
using Microsoft.EntityFrameworkCore;

#endregion

namespace GainsTracker.Data.UserProfiles;

public delegate Expression<Func<T, object>> PropertyToInclude<T>();

public class UserProfileRepository(GainsDbContextFactory contextFactory)
    : GenericRepository<UserProfile>(contextFactory), IUserProfileRepository
{
    private readonly GainsDbContextFactory _contextFactory = contextFactory;

    public async Task UpdateUserProfileByUserHandle(string userHandle, UpdateUserProfileDto userProfileDto)
    {
        var current = await GetUserProfileEntityByUserHandle(userHandle);
        current.Description = userProfileDto.Description ?? current.Description;
        current.DisplayName = userProfileDto.DisplayName ?? current.DisplayName;
        current.Icon.Url = userProfileDto.IconUrl ?? current.Icon.Url;

        if (userProfileDto.IconColorHex != null)
            current.Icon.PictureColor = ColorTranslator.FromHtml(userProfileDto.IconColorHex).ToArgb();

        await UpdateAsync(current);
    }

    public async Task<List<Measurement>> GetPinnedPBs(string userHandle)
    {
        var profileEntity = await GetUserProfileByUserHandle(userHandle, () => up => up.PinnedPBs);
        return [.. profileEntity.PinnedPBs];
    }

    public async Task AddAndRemovePBs(string userHandle, UpdatePinnedPBsDto pinnedPBsDto)
    {
        await using var context = _contextFactory.CreateDbContext();

        var userProfileEntity = await GetUserProfileByUserHandle(userHandle, () => up => up.PinnedPBs);
        List<Measurement> toAdd = [];
        List<Measurement> toRemove = [];

        pinnedPBsDto.AddPBs.ForEach(pb =>
        {
            var measurement = context.Measurements.FirstOrDefault(measurement => measurement.Id == pb);

            if (measurement == null)
                throw new NotFoundException($"Measurement with id {pb} was not found.");

            toAdd.Add(measurement);
        });

        pinnedPBsDto.RemovePBs.ForEach(pb =>
        {
            var measurement = context.Measurements.FirstOrDefault(measurement => measurement.Id == pb);
            if (measurement == null)
                throw new NotFoundException($"Measurement with id {pb} was not found.");

            toRemove.Add(measurement);
        });

        userProfileEntity.PinnedPBs.AddRange(toAdd);
        userProfileEntity.PinnedPBs = userProfileEntity.PinnedPBs.Except(toRemove).ToList();

        await UpdateAsync(userProfileEntity);
    }

    public async Task<UserProfile> GetUserProfileByUserHandle(string userHandle)
    {
        await using var context = _contextFactory.CreateDbContext();

        var gainsId = await GetGainsIdByUserHandle(userHandle);
        var userProfile = await context.UserProfiles
            .Include(up => up.Icon)
            .FirstOrDefaultAsync(e => e.GainsAccountId == gainsId);

        if (userProfile == null)
            throw new NotFoundException($"UserProfile for user with id {gainsId} was not found.");

        return userProfile;
    }

    public async Task<UserProfile> GetUserProfileEntityByUserHandle(string userHandle)
    {
        await using var context = _contextFactory.CreateDbContext();

        var gainsId = await GetGainsIdByUserHandle(userHandle);
        var userProfile = await context.UserProfiles
            .Include(up => up.Icon)
            .FirstOrDefaultAsync(e => e.GainsAccountId == gainsId);

        if (userProfile == null)
            throw new NotFoundException($"UserProfile for user with id {gainsId} was not found.");

        return userProfile;
    }

    private async Task<UserProfile> GetUserProfileByUserHandle(string userHandle,
        params PropertyToInclude<UserProfile>[] properties)
    {
        await using var context = _contextFactory.CreateDbContext();

        var gainsId = await GetGainsIdByUserHandle(userHandle);

        // If no include expressions are provided, set a default one for the Icon.
        var includes = properties.Length != 0
            ? properties
            : [() => up => up.Icon];

        var query = context.UserProfiles.AsQueryable();

        foreach (var property in includes)
            // Use the property expression to include it in the query.
            query = query.Include(property());

        var userProfile = await query.FirstOrDefaultAsync(e => e.GainsAccountId == gainsId);

        if (userProfile == null)
            throw new NotFoundException($"User profile for gains account {gainsId} was not found.");

        return userProfile;
    }
}

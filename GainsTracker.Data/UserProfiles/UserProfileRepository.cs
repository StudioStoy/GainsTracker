using System.Drawing;
using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Core.UserProfiles.Interfaces.Repositories;
using GainsTracker.Core.UserProfiles.Models;
using GainsTracker.Core.Workouts.Models.Measurements;
using GainsTracker.Data.Shared;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.Data.UserProfiles;

public class UserProfileRepository(GainsDbContextFactory contextFactory)
    : GenericRepository<UserProfile>(contextFactory), IUserProfileRepository
{
    private readonly GainsDbContextFactory _contextFactory = contextFactory;

    public async Task UpdateUserProfileByUserHandle(Guid gainsId, UpdateUserProfileDto userProfileDto)
    {
        var current = await GetUserProfileEntityByGainsId(gainsId);
        current.Description = userProfileDto.Description ?? current.Description;
        current.DisplayName = userProfileDto.DisplayName ?? current.DisplayName;
        current.Icon.Url = userProfileDto.IconUrl ?? current.Icon.Url;

        if (userProfileDto.IconColorHex != null)
            current.Icon.PictureColor = ColorTranslator.FromHtml(userProfileDto.IconColorHex).ToArgb();

        await UpdateAsync(current);
    }

    public async Task<List<Measurement>> GetPinnedPBs(Guid gainsId)
    {
        var profileEntity = await GetUserProfileByGainsId(gainsId, () => up => up.PinnedPBs);
        return [.. profileEntity.PinnedPBs];
    }

    public async Task AddAndRemovePBs(Guid gainsId, UpdatePinnedPBsDto pinnedPBsDto)
    {
        await using var context = _contextFactory.CreateDbContext();

        var userProfileEntity = await GetUserProfileByGainsId(gainsId, () => up => up.PinnedPBs);
        List<Measurement> toAdd = [];
        List<Measurement> toRemove = [];

        pinnedPBsDto.AddPBs.ForEach(pb =>
        {
            Guid pbGuid = Guid.Parse(pb);
            var measurement = context.Measurements.FirstOrDefault(measurement => measurement.Id == pbGuid);

            if (measurement == null)
                throw new NotFoundException($"Measurement with id {pb} was not found.");

            toAdd.Add(measurement);
        });

        pinnedPBsDto.RemovePBs.ForEach(pb =>
        {
            var pbGuid = Guid.Parse(pb);
            var measurement = context.Measurements.FirstOrDefault(measurement => measurement.Id == pbGuid);
            if (measurement == null)
                throw new NotFoundException($"Measurement with id {pb} was not found.");

            toRemove.Add(measurement);
        });

        userProfileEntity.PinnedPBs.AddRange(toAdd);
        userProfileEntity.PinnedPBs = userProfileEntity.PinnedPBs.Except(toRemove).ToList();

        await UpdateAsync(userProfileEntity);
    }

    public async Task<UserProfile> GetUserProfileByGainsId(Guid gainsId)
    {
        await using var context = _contextFactory.CreateDbContext();

        var userProfile = await context.UserProfiles
            .Include(up => up.Icon)
            .FirstOrDefaultAsync(e => e.GainsAccountId == gainsId);

        if (userProfile == null)
            throw new NotFoundException($"UserProfile for user with id {gainsId} was not found.");

        return userProfile;
    }

    private async Task<UserProfile> GetUserProfileEntityByGainsId(Guid gainsId)
    {
        await using var context = _contextFactory.CreateDbContext();

        var userProfile = await context.UserProfiles
            .Include(up => up.Icon)
            .FirstOrDefaultAsync(e => e.GainsAccountId == gainsId);

        if (userProfile == null)
            throw new NotFoundException($"UserProfile for user with id {gainsId} was not found.");

        return userProfile;
    }

    private async Task<UserProfile> GetUserProfileByGainsId(Guid gainsId,
        params IncludeProperty<UserProfile>[] properties)
    {
        await using var context = _contextFactory.CreateDbContext();

        var query = context.UserProfiles.AsQueryable();
        query = properties.Aggregate(query, (current, property) => current.Include(property()));

        var userProfile = await query.FirstOrDefaultAsync(e => e.GainsAccountId == gainsId);

        if (userProfile == null)
            throw new NotFoundException($"User profile for gains account {gainsId} was not found.");

        return userProfile;
    }
}

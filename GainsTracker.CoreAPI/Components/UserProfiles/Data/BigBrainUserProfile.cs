using System.Drawing;
using GainsTracker.Common.Exceptions;
using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.CoreAPI.Components.UserProfiles.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;
using GainsTracker.CoreAPI.Database;
using GainsTracker.CoreAPI.Shared;
using Microsoft.EntityFrameworkCore;

namespace GainsTracker.CoreAPI.Components.UserProfiles.Data;

public class BigBrainUserProfile : BigBrain
{
    public BigBrainUserProfile(AppDbContext context) : base(context)
    {
    }

    public UserProfile GetUserProfileByUserHandle(string userHandle, params PropertyToInclude<UserProfile>[] properties)
    {
        string gainsId = GetGainsIdByUsername(userHandle);

        // If there's no include expression provided, get the standard user profile with icon.
        if (properties.Length <= 0)
            properties.ToList().Add(() => up => up.Icon);

        IQueryable<UserProfile> query = Context.UserProfiles.AsQueryable();
        foreach (PropertyToInclude<UserProfile> property in properties)
            query = query.Include(property.Invoke());

        return query.FirstOrDefault(e => e.GainsAccountId == gainsId)
               ?? throw new NotFoundException($"User profile for gains account {gainsId} was not found.");
    }

    public void UpdateUserProfileByUserHandle(string userHandle, UpdateUserProfileDto userProfileDto)
    {
        UserProfile current = GetUserProfileByUserHandle(userHandle);

        current.Description = userProfileDto.Description ?? current.Description;
        current.DisplayName = userProfileDto.DisplayName ?? current.DisplayName;
        current.Icon.Url = userProfileDto.IconUrl ?? current.Icon.Url;

        if (userProfileDto.IconColorHex != null)
            current.Icon.PictureColor = ColorTranslator.FromHtml(userProfileDto.IconColorHex).ToArgb();

        SaveContext();
    }

    public List<MeasurementDto> GetPinnedPBs(string userHandle)
    {
        UserProfile profile = GetUserProfileByUserHandle(userHandle, () => up => up.PinnedPBs);
        return profile.PinnedPBs.ToDtoList<Measurement, MeasurementDto>();
    }

    public void AddAndRemovePBs(string userHandle, UpdatePinnedPBsDto pinnedPBsDto)
    {
        UserProfile userProfile = GetUserProfileByUserHandle(userHandle, () => up => up.PinnedPBs);
        List<Measurement> toAdd = new();
        List<Measurement> toRemove = new();

        pinnedPBsDto.AddPBs.ForEach(pb =>
        {
            Measurement? measurement = Context.Measurements.FirstOrDefault(measurement => measurement.Id == pb);
            if (measurement == null)
                throw new NotFoundException($"Measurement with id {pb} was not found.");

            toAdd.Add(measurement);
        });

        pinnedPBsDto.RemovePBs.ForEach(pb =>
        {
            Measurement? measurement = Context.Measurements.FirstOrDefault(measurement => measurement.Id == pb);
            if (measurement == null)
                throw new NotFoundException($"Measurement with id {pb} was not found.");

            toRemove.Add(measurement);
        });

        userProfile.PinnedPBs.AddRange(toAdd);
        userProfile.PinnedPBs = userProfile.PinnedPBs.Except(toRemove).ToList();

        SaveContext();
    }
}

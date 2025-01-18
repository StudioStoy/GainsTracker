using System.Drawing;
using System.Reflection;
using GainsTracker.Common.Models.Friends.Dto;
using GainsTracker.Common.Models.UserDtos;
using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.UserProfiles.Models;
using GainsTracker.Core.Users.Models;
using GainsTracker.Core.Workouts.Models.Measurements;
using GainsTracker.Core.Workouts.Models.Workouts;

namespace GainsTracker.Core;

public static class DtoExtensions
{
    public static List<TDto> ToDtoList<TModel, TDto>(this IEnumerable<TModel> items)
    {
        List<TDto> dtoList = [];

        foreach (var item in items)
        {
            if (item == null)
                continue;

            // Finds the ToDto() extension method based on the item's type.
            var toDtoMethod = typeof(DtoExtensions)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(
                    method => method.Name == "ToDto"
                              && method.GetParameters().Length == 1
                              && method.GetParameters()[0].ParameterType == typeof(TModel)
                ) ?? throw new NotSupportedException(
                $"ToDto method not found for type {typeof(TModel).Name}");

            // Invokes the ToDto() extension method and adds the result to the DTO list.
            var dtoItem = (TDto?)toDtoMethod.Invoke(null, [item]);
            if (dtoItem != null)
                dtoList.Add(dtoItem);
        }

        return dtoList;
    }
    
    // User
    public static UserDto ToDto(this User user) =>
        new(
            Id: user.Id,
            AuthId: user.AuthIdentifier,
            GainsAccountId: user.GainsAccountId,
            UserHandle: user.Handle,
            Email: user.Email
        );

    // FriendRequest
    public static FriendRequestDto ToDto(this FriendRequest request)
    {
        var byName = request.Requester.UserProfile.DisplayName;
        var toName = request.Recipient.UserProfile.DisplayName;

        byName = !string.IsNullOrEmpty(byName)
            ? byName + $" (@{request.Requester.UserHandle})"
            : request.Requester.UserHandle;
        toName = !string.IsNullOrEmpty(toName)
            ? toName + $" (@{request.Recipient.UserHandle})"
            : request.Recipient.UserHandle;

        return new FriendRequestDto
        (
            request.Id,
            RequesterId: request.RequesterId,
            RecipientId: request.RecipientId,
            RequesterName: byName,
            RecipientName: toName,
            RequestTime: request.RequestTime.ToLongDateString(),
            Status: request.Status.ToString()
        );
    }

    // Friend
    public static FriendDto ToDto(this Friend friend) =>
        new(
            Id: friend.Id.ToString(),
            UserHandle: friend.Handle,
            FriendsSince: friend.FriendsSince
        );

    // UserProfile
    public static UserProfileDto ToDto(this UserProfile userProfile)
    {
        return new UserProfileDto
        (
            Description: userProfile.Description,
            IconUrl: userProfile.Icon.Url,
            IconColor: ColorTranslator.ToHtml(Color.FromArgb(userProfile.Icon.PictureColor)),
            DisplayName: userProfile.DisplayName,
            PinnedPBs: userProfile.PinnedPBs.Select(pb => new MeasurementDto
            (
                Id: pb.Id.ToString(),
                WorkoutId: string.Empty,
                Category: pb.Category,
                TimeOfRecord: pb.TimeOfRecord,
                Notes: pb.Notes,
                Data: MeasurementFactory.SerializeMeasurementToJson(pb)
            )).ToList()
        );
    }

    // Workout
    public static WorkoutDto ToDto(this Workout workout)
    {
        MeasurementDto? bestMeasurement = null;

        if (workout.PersonalBest != null)
            bestMeasurement = new MeasurementDto
            (
                Id: workout.PersonalBest.Id.ToString(),
                WorkoutId: workout.Id.ToString(),
                TimeOfRecord: workout.PersonalBest.TimeOfRecord,
                Category: workout.PersonalBest.Category,
                Notes: workout.PersonalBest.Notes,
                Data: MeasurementFactory.SerializeMeasurementToJson(workout.PersonalBest)
            );

        return new WorkoutDto
        (
            Id: workout.Id,
            GainsAccountId: workout.GainsAccountId,
            Type: workout.Type,
            Category: workout.Category,
            PersonalBest: bestMeasurement
        );
    }

    // Measurement
    public static MeasurementDto ToDto(this Measurement measurement) =>
        new(
            Id: measurement.Id.ToString(),
            WorkoutId: string.Empty,
            Category: measurement.Category,
            TimeOfRecord: measurement.TimeOfRecord,
            Notes: measurement.Notes,
            Data: MeasurementFactory.SerializeMeasurementToJson(measurement)
        );
}

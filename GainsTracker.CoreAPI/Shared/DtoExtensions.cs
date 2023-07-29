using System.Drawing;
using System.Reflection;
using GainsTracker.Common.Models.Friends.Dto;
using GainsTracker.Common.Models.UserProfiles;
using GainsTracker.Common.Models.Workouts.Dto;
using GainsTracker.CoreAPI.Components.Friends.Models;
using GainsTracker.CoreAPI.Components.UserProfiles.Models;
using GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;
using GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;

namespace GainsTracker.CoreAPI.Shared;

public static class DtoExtensions
{
    public static List<TDto> ToDtoList<TModel, TDto>(this IEnumerable<TModel> items)
    {
        List<TDto> dtoList = new List<TDto>();

        foreach (TModel item in items)
        {
            // Finds the ToDto() extension method based on the item's type.
            MethodInfo? toDtoMethod = typeof(DtoExtensions)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(
                    method => method.Name == "ToDto"
                              && method.GetParameters().Length == 1
                              && method.GetParameters()[0].ParameterType == typeof(TModel)
                );

            if (toDtoMethod == null)
                throw new NotSupportedException(
                    $"ToDto method not found for type {typeof(TModel).Name}");

            // Invokes the ToDto() extension method and adds the result to the DTO list.
            TDto? dtoItem = (TDto) toDtoMethod.Invoke(null, new object[] { item });
            dtoList.Add(dtoItem);
        }

        return dtoList;
    }

    // Friend
    public static FriendRequestDto ToDto(this FriendRequest request)
    {
        string byName = request.Requester.UserProfile.DisplayName;
        string toName = request.Recipient.UserProfile.DisplayName;

        byName = !string.IsNullOrEmpty(byName)
            ? byName + $" (@{request.Requester.UserHandle})"
            : request.Requester.UserHandle;
        toName = !string.IsNullOrEmpty(toName)
            ? toName + $" (@{request.Recipient.UserHandle})"
            : request.Recipient.UserHandle;

        return new FriendRequestDto
        (
            request.Id,
            requesterId: request.RequesterId,
            recipientId: request.RecipientId,
            requesterName: byName,
            recipientName: toName,
            requestTime: request.RequestTime.ToLongDateString(),
            status: request.Status.ToString()
        );
    }

    // UserProfile
    public static UserProfileDto ToDto(this UserProfile userProfile)
    {
        return new UserProfileDto
        {
            Description = userProfile.Description,
            IconUrl = userProfile.Icon.Url,
            IconColor = ColorTranslator.ToHtml(Color.FromArgb(userProfile.Icon.PictureColor)),
            DisplayName = userProfile.DisplayName,
            PinnedPBs = userProfile.PinnedPBs.Select(pb => new MeasurementDto
            {
                Id = pb.Id,
                WorkoutId = pb.WorkoutId,
                Category = pb.Category,
                TimeOfRecord = pb.TimeOfRecord,
                Notes = pb.Notes,
                Data = MeasurementFactory.SerializeMeasurementToJson(pb)
            }).ToList()
        };
    }

    // Workout
    public static WorkoutDto ToDto(this Workout workout)
    {
        MeasurementDto? bestMeasurement = null;

        if (workout.PersonalBest != null)
            bestMeasurement = new MeasurementDto
            {
                Id = workout.PersonalBest.Id,
                WorkoutId = workout.PersonalBest.WorkoutId,
                TimeOfRecord = workout.PersonalBest.TimeOfRecord,
                Category = workout.PersonalBest.Category,
                Notes = workout.PersonalBest.Notes,
                Data = MeasurementFactory.SerializeMeasurementToJson(workout.PersonalBest)
            };

        return new WorkoutDto(workout.GainsAccountId)
        {
            Id = workout.Id,
            Type = workout.Type,
            Category = workout.Category,
            PersonalBest = bestMeasurement
        };
    }

    // Measurement
    public static MeasurementDto ToDto(this Measurement measurement)
    {
        return new MeasurementDto
        {
            Id = measurement.Id,
            WorkoutId = measurement.WorkoutId,
            Category = measurement.Category,
            TimeOfRecord = measurement.TimeOfRecord,
            Notes = measurement.Notes,
            Data = MeasurementFactory.SerializeMeasurementToJson(measurement)
        };
    }
}

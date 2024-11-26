using System.Linq.Expressions;
using GainsTracker.Core.Friends.Models;
using GainsTracker.Core.Gains.Models;
using GainsTracker.Core.HealthMetrics.Models;
using GainsTracker.Core.UserProfiles.Models;
using GainsTracker.Core.Workouts.Models.Workouts;
using GainsTracker.Data.Friends;
using GainsTracker.Data.Friends.Entities;
using GainsTracker.Data.Gains;
using GainsTracker.Data.Gains.Entities;
using GainsTracker.Data.HealthMetrics;
using GainsTracker.Data.HealthMetrics.Entities;
using GainsTracker.Data.UserProfiles;
using GainsTracker.Data.UserProfiles.Entities;
using GainsTracker.Data.Workouts;
using GainsTracker.Data.Workouts.Entities;

namespace GainsTracker.Data;

public static class MappingExtensions
{
    public static TEntity AsEntity<TDomain, TEntity>(this TDomain domain)
        where TEntity : class
    {
        return domain switch
        {
            Friend friend => friend.ToEntity() as TEntity,
            GainsAccount gainsAccount => gainsAccount.ToEntity() as TEntity,
            HealthMetric healthMetric => healthMetric.ToEntity() as TEntity,
            UserProfile userProfile => userProfile.ToEntity() as TEntity,
            Workout workout => workout.ToEntity() as TEntity,
            _ => throw new InvalidOperationException($"Mapping not defined for {typeof(TDomain)} to {typeof(TEntity)}.")
        } ?? throw new InvalidOperationException();
    }

    public static TDomain AsDomain<TDomain, TEntity>(this TEntity entity)
        where TDomain : class
    {
        return entity switch
        {
            FriendEntity friendEntity => friendEntity.ToModel() as TDomain,
            GainsAccountEntity gainsAccountEntity => gainsAccountEntity.ToModel() as TDomain,
            HealthMetricEntity healthMetricEntity => healthMetricEntity.ToModel() as TDomain,
            UserProfileEntity userProfileEntity => userProfileEntity.ToModel() as TDomain,
            WorkoutEntity workoutEntity => workoutEntity.ToModel() as TDomain,
            _ => throw new InvalidOperationException($"Mapping not defined for {typeof(TEntity)} to {typeof(TDomain)}.")
        } ?? throw new InvalidOperationException();
    }
}

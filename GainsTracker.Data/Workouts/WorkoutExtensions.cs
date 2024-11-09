using GainsTracker.Core.Components.Workouts.Models.Measurements;
using GainsTracker.Core.Components.Workouts.Models.Workouts;

namespace GainsTracker.Data.Workouts
{
    public static class WorkoutExtensions
    {
        // Workout
        public static Workout MapToModel(this WorkoutEntity entity)
        {
            return new Workout(entity.GainsAccountId, entity.Type, entity.Category,
                entity.Measurements.Select(m => m.MapToModel()).ToList())
            {
                Id = entity.Id,
                PersonalBest = entity.PersonalBest?.MapToModel()
            };
        }

        public static WorkoutEntity MapToEntity(this Workout model)
        {
            return new WorkoutEntity
            {
                Id = model.Id,
                GainsAccountId = model.GainsAccountId,
                Type = model.Type,
                Category = model.Category,
                PersonalBest = model.PersonalBest?.MapToEntity(),
                Measurements = model.Measurements.Select(m => m.MapToEntity()).ToList()
            };
        }

        // Measurement
        public static Measurement MapToModel(this MeasurementEntity entity)
        {
            return entity switch
            {
                StrengthMeasurementEntity strength => new StrengthMeasurement
                {
                    Id = strength.Id,
                    WorkoutId = strength.WorkoutId,
                    UserProfileId = strength.UserProfileId,
                    TimeOfRecord = strength.TimeOfRecord,
                    Notes = strength.Notes,
                    IsInGoal = strength.IsInGoal,
                    WeightUnit = strength.WeightUnit,
                    Weight = strength.Weight,
                    Reps = strength.Reps
                },
                TimeAndDistanceEnduranceMeasurementEntity endurance => new TimeAndDistanceEnduranceMeasurement
                {
                    Id = endurance.Id,
                    WorkoutId = endurance.WorkoutId,
                    UserProfileId = endurance.UserProfileId,
                    TimeOfRecord = endurance.TimeOfRecord,
                    Notes = endurance.Notes,
                    IsInGoal = endurance.IsInGoal,
                    Time = endurance.Time,
                    DistanceUnit = endurance.DistanceUnit,
                    Distance = endurance.Distance
                },
                TimeEnduranceMeasurementEntity timeEndurance => new TimeEnduranceMeasurement
                {
                    Id = timeEndurance.Id,
                    WorkoutId = timeEndurance.WorkoutId,
                    UserProfileId = timeEndurance.UserProfileId,
                    TimeOfRecord = timeEndurance.TimeOfRecord,
                    Notes = timeEndurance.Notes,
                    IsInGoal = timeEndurance.IsInGoal,
                    Time = timeEndurance.Time
                },
                RepsMeasurementEntity reps => new RepsMeasurement
                {
                    Id = reps.Id,
                    WorkoutId = reps.WorkoutId,
                    UserProfileId = reps.UserProfileId,
                    TimeOfRecord = reps.TimeOfRecord,
                    Notes = reps.Notes,
                    IsInGoal = reps.IsInGoal,
                    Reps = reps.Reps
                },
                GeneralMeasurementEntity general => new GeneralMeasurement
                {
                    Id = general.Id,
                    WorkoutId = general.WorkoutId,
                    UserProfileId = general.UserProfileId,
                    TimeOfRecord = general.TimeOfRecord,
                    Notes = general.Notes,
                    IsInGoal = general.IsInGoal,
                    GeneralAchievement = general.GeneralAchievement
                },
                _ => throw new ArgumentOutOfRangeException("Unknown MeasurementEntity type")
            };
        }

        public static MeasurementEntity MapToEntity(this Measurement model)
        {
            return model switch
            {
                StrengthMeasurement strength => new StrengthMeasurementEntity
                {
                    Id = strength.Id,
                    WorkoutId = strength.WorkoutId,
                    UserProfileId = strength.UserProfileId,
                    TimeOfRecord = strength.TimeOfRecord,
                    Notes = strength.Notes,
                    IsInGoal = strength.IsInGoal,
                    WeightUnit = strength.WeightUnit,
                    Weight = strength.Weight,
                    Reps = strength.Reps
                },
                TimeAndDistanceEnduranceMeasurement endurance => new TimeAndDistanceEnduranceMeasurementEntity
                {
                    Id = endurance.Id,
                    WorkoutId = endurance.WorkoutId,
                    UserProfileId = endurance.UserProfileId,
                    TimeOfRecord = endurance.TimeOfRecord,
                    Notes = endurance.Notes,
                    IsInGoal = endurance.IsInGoal,
                    Time = endurance.Time,
                    DistanceUnit = endurance.DistanceUnit,
                    Distance = endurance.Distance
                },
                TimeEnduranceMeasurement timeEndurance => new TimeEnduranceMeasurementEntity
                {
                    Id = timeEndurance.Id,
                    WorkoutId = timeEndurance.WorkoutId,
                    UserProfileId = timeEndurance.UserProfileId,
                    TimeOfRecord = timeEndurance.TimeOfRecord,
                    Notes = timeEndurance.Notes,
                    IsInGoal = timeEndurance.IsInGoal,
                    Time = timeEndurance.Time
                },
                RepsMeasurement reps => new RepsMeasurementEntity
                {
                    Id = reps.Id,
                    WorkoutId = reps.WorkoutId,
                    UserProfileId = reps.UserProfileId,
                    TimeOfRecord = reps.TimeOfRecord,
                    Notes = reps.Notes,
                    IsInGoal = reps.IsInGoal,
                    Reps = reps.Reps
                },
                GeneralMeasurement general => new GeneralMeasurementEntity
                {
                    Id = general.Id,
                    WorkoutId = general.WorkoutId,
                    UserProfileId = general.UserProfileId,
                    TimeOfRecord = general.TimeOfRecord,
                    Notes = general.Notes,
                    IsInGoal = general.IsInGoal,
                    GeneralAchievement = general.GeneralAchievement
                },
                _ => throw new ArgumentOutOfRangeException("Unknown Measurement type")
            };
        }
    }
}

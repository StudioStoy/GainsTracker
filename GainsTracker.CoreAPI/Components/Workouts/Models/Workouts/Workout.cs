﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.CoreAPI.Components.Workouts.Models.Measurements;

namespace GainsTracker.CoreAPI.Components.Workouts.Models.Workouts;

[Table("workout")]
public class Workout
{
    // For Hibernate
    protected Workout()
    {
        GainsAccountId = string.Empty;
    }

    public Workout(string gainsAccountId, WorkoutType type, ExerciseCategory category, List<Measurement> measurements)
    {
        GainsAccountId = gainsAccountId;
        Type = type;
        Category = category;
        Measurements = measurements;
        if (measurements.Any())
            PersonalBest = measurements.First();
    }

    public WorkoutType Type { get; set; }
    public ExerciseCategory Category { get; set; }
    [ForeignKey("BestMeasurementId")] public Measurement? PersonalBest { get; set; }
    public List<Measurement> Measurements { get; set; } = new();

    public void AddNewMeasurement(Measurement measurement)
    {
        CheckAndUpdatePersonalBest(measurement, PersonalBest);
        Measurements.Add(measurement);
    }

    private void CheckAndUpdatePersonalBest(Measurement newMeasurement, Measurement? oldPersonalBest)
    {
        if (oldPersonalBest == null)
        {
            PersonalBest = newMeasurement;
            return;
        }

        if (newMeasurement.GetType() != oldPersonalBest.GetType())
            throw new ArgumentException("Cannot compare measurements as they're not the same type.");

        // Sets the PersonalBest to the new measurement if its values are higher, otherwise keep the old one.
        PersonalBest = MeasurementFactory.GetValidator<Measurement>(Type, oldPersonalBest, newMeasurement)
            .CheckIfImproved()
            ? newMeasurement
            : oldPersonalBest;
    }

    #region Relations

    [JsonIgnore] public string Id { get; set; } = Guid.NewGuid().ToString();
    [JsonIgnore] public string GainsAccountId { get; set; }

    #endregion
}

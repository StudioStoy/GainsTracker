using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GainsTracker.CoreAPI.Components.Gains.Models.Measurements;

namespace GainsTracker.CoreAPI.Components.Gains.Models.Workouts;

[Table("workout")]
public class Workout
{
    protected Workout()
    {
    }

    public Workout(string gainsAccountId, WorkoutType type, List<Measurement> measurements)
    {
        GainsAccountId = gainsAccountId;
        WorkoutType = type;
        Measurements = measurements;
        if (measurements.Any())
            PersonalBest = measurements.First();
    }

    public WorkoutType WorkoutType { get; set; }
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

        // Sets the PersonalBest to the new measurement if its values are higher,
        // otherwise keep the old one. 
        PersonalBest = newMeasurement switch
        {
            StrengthMeasurement measurement =>
                measurement.Weight > (oldPersonalBest as StrengthMeasurement)!.Weight ? newMeasurement : oldPersonalBest,
            SimpleRepMeasurement measurement =>
                measurement.Reps > (oldPersonalBest as SimpleRepMeasurement)!.Reps ? newMeasurement : oldPersonalBest,
            SimpleEnduranceMeasurement measurement =>
                measurement.Time > (oldPersonalBest as SimpleEnduranceMeasurement)!.Time ? newMeasurement : oldPersonalBest,
            RunningEnduranceMeasurement measurement =>
                measurement.Time > (oldPersonalBest as RunningEnduranceMeasurement)!.Time ? newMeasurement : oldPersonalBest,
            _ => throw new ArgumentOutOfRangeException(nameof(newMeasurement), newMeasurement, "This type is not supported.")
        };
    }

    #region Relations

    [JsonIgnore] public string Id { get; set; } = Guid.NewGuid().ToString();
    [JsonIgnore] public string GainsAccountId { get; set; }

    #endregion
}

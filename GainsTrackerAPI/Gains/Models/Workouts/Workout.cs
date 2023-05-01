using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GainsTrackerAPI.Gains.Models.Measurements;

namespace GainsTrackerAPI.Gains.Models;

[Table("workout")]
public class Workout
{
    protected Workout()
    {
    }

    public Workout(string gainsAccountId, WorkoutType type, List<Measurement> measurements)
    {
        if (measurements.Count <= 0) throw new ArgumentException("A workout must have at least one measurement");

        GainsAccountId = gainsAccountId;
        WorkoutType = type;
        Measurements = measurements;
        PersonalBest = measurements.First();
    }

    public WorkoutType WorkoutType { get; set; }

    [ForeignKey("MeasurementId")] public Measurement PersonalBest { get; set; }
    public List<Measurement> Measurements { get; set; } = new();

    public void CheckPersonalBest(Measurement newMeasurement, Measurement oldPersonalBest)
    {
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

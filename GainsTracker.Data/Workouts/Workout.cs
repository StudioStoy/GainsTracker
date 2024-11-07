using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.Data.Workouts;

[Table("workout")]
public class Workout
{
    // For Hibernate
    private Workout() {}

    public WorkoutType Type { get; set; }
    
    public ExerciseCategory Category { get; set; }
    
    [ForeignKey("BestMeasurementId")] 
    public Measurement? PersonalBest { get; set; }
    public List<Measurement> Measurements { get; set; } = [];

    #region Relations

    [JsonIgnore] public string Id { get; set; } = string.Empty;
    [JsonIgnore] public string GainsAccountId { get; set; } = string.Empty;

    #endregion
}

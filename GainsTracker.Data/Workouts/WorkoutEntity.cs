using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GainsTracker.Common.Models.Workouts;
using GainsTracker.Data.Shared;

namespace GainsTracker.Data.Workouts;

[Table("workout")]
public class WorkoutEntity
{
    public WorkoutType Type { get; set; }
    
    public ExerciseCategory Category { get; set; }
    
    [ForeignKey("BestMeasurementId")] 
    public MeasurementEntity? PersonalBest { get; set; }
    public List<MeasurementEntity> Measurements { get; set; } = [];

    public Guid Id { get; set; }
    public Guid GainsAccountId { get; set; }

}

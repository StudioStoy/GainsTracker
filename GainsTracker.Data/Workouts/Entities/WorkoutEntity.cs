using System.ComponentModel.DataAnnotations.Schema;
using GainsTracker.Common.Models.Workouts;

namespace GainsTracker.Data.Workouts.Entities;

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

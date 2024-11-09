using GainsTracker.Core.Components.HealthMetrics.Models;

namespace GainsTracker.Data.HealthMetrics
{
    public static class HealthMetricExtensions
    {
        // HealthMetric
        public static HealthMetric MapToModel(this HealthMetricEntity entity)
        {
            return entity switch
            {
                ProteinHealthMetricEntity proteinEntity => new ProteinHealthMetric
                {
                    Id = proteinEntity.Id,
                    Type = proteinEntity.Type,
                    LoggingDate = proteinEntity.LoggingDate,
                    IsInGoal = proteinEntity.IsInGoal,
                    ProteinIntake = proteinEntity.ProteinIntake
                },
                WeightHealthMetricEntity weightEntity => new WeightHealthMetric
                {
                    Id = weightEntity.Id,
                    Type = weightEntity.Type,
                    LoggingDate = weightEntity.LoggingDate,
                    IsInGoal = weightEntity.IsInGoal,
                    Weight = weightEntity.Weight
                },
                LiterWaterHealthMetricEntity waterEntity => new LiterWaterHealthMetric
                {
                    Id = waterEntity.Id,
                    Type = waterEntity.Type,
                    LoggingDate = waterEntity.LoggingDate,
                    IsInGoal = waterEntity.IsInGoal,
                    Liters = waterEntity.Liters
                },
                _ => throw new InvalidOperationException($"Unknown HealthMetricEntity type: {entity.GetType().Name}")
            };
        }
        
        public static HealthMetricEntity MapToEntity(this HealthMetric model)
        {
            return model switch
            {
                ProteinHealthMetric proteinModel => new ProteinHealthMetricEntity
                {
                    Id = proteinModel.Id,
                    Type = proteinModel.Type,
                    LoggingDate = proteinModel.LoggingDate,
                    IsInGoal = proteinModel.IsInGoal,
                    ProteinIntake = proteinModel.ProteinIntake
                },
                WeightHealthMetric weightModel => new WeightHealthMetricEntity
                {
                    Id = weightModel.Id,
                    Type = weightModel.Type,
                    LoggingDate = weightModel.LoggingDate,
                    IsInGoal = weightModel.IsInGoal,
                    Weight = weightModel.Weight
                },
                LiterWaterHealthMetric waterModel => new LiterWaterHealthMetricEntity
                {
                    Id = waterModel.Id,
                    Type = waterModel.Type,
                    LoggingDate = waterModel.LoggingDate,
                    IsInGoal = waterModel.IsInGoal,
                    Liters = waterModel.Liters
                },
                _ => throw new InvalidOperationException($"Unknown HealthMetric type: {model.GetType().Name}")
            };
        }
    }
}

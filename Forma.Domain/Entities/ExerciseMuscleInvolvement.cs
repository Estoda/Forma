using Forma.Domain.Common;
using Forma.Domain.Enums;

namespace Forma.Domain.Entities;

public class ExerciseMuscleInvolvement : BaseEntity
{
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    
    public Muscle Muscle { get; set; }
    public double PercentageContribution { get; set; }
}

using Forma.Domain.Common;
using Forma.Domain.Enums;

namespace Forma.Domain.Entities;

public class Set : BaseEntity
{
    public Guid WorkoutExerciseId { get; set; }
    public WorkoutExercise WorkoutExercise { get; set; } = null!;

    public int SetNumber { get; set; }
    public int Reps { get; set; }
    public double Weight { get; set; }
    public int? RestSeconds { get; set; }

    public SetType Type { get; set; } = SetType.Normal;
    public double? RPE { get; set; }
}

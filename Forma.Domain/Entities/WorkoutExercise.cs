using Forma.Domain.Common;
using Forma.Domain.Enums;

namespace Forma.Domain.Entities;

public class WorkoutExercise : BaseEntity
{
    public Guid WorkoutId { get; set; }
    public Workout Workout { get; set; } = null!;
    public Guid ExerciseId { get; set; }
    public Exercise Exercise { get; set; } = null!;
    public int Order { get; set; }

    public ICollection<Set> Sets { get; set; } = new List<Set>();
}


using Forma.Domain.Common;

namespace Forma.Domain.Entities;

public class Exercise : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Equipment { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;

    public ICollection<ExerciseMuscleInvolvement> MuscleInvolvements { get; set; } = new List<ExerciseMuscleInvolvement>();
    public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
}

using Forma.Domain.Common;
using Forma.Domain.Enums;

namespace Forma.Domain.Entities;

public class Workout : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    public string? Notes { get; set; }

    public ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();

    public double? DurationMinutes => EndTime.HasValue
    ? (EndTime.Value - StartTime).TotalMinutes
    : null;
}

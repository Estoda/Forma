using Forma.Domain.Common;
using Forma.Domain.Enums;

namespace Forma.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int Age { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public Gender Gender { get; set; }
    public string Goal { get; set; } = string.Empty;
    public DateTime TrainingSince { get; set; }

    public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
}

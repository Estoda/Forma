using Forma.Domain.Common;

namespace Forma.Domain.Entities;

public class WaterIntake : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime LoggedAt { get; set; }
    public double AmountMl { get; set; }
}

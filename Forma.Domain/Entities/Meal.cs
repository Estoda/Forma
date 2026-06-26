using Forma.Domain.Common;
using Forma.Domain.Enums;

namespace Forma.Domain.Entities;

public class Meal : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime LoggedAt { get; set; }
    public MealType Type { get; set; }
    public string? Notes { get; set; }

    public ICollection<MealFoodItem> MealFoodItems { get; set; } = new List<MealFoodItem>();
}

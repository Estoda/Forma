using Forma.Domain.Common;

namespace Forma.Domain.Entities;

public class MealFoodItem : BaseEntity
{
    public Guid MealId { get; set; }
    public Meal Meal { get; set; } = null!;
    
    public Guid FoodItemId { get; set; }
    public FoodItem FoodItem { get; set; } = null!;

    public double QuantityGrams { get; set; }
}

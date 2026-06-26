using Forma.Domain.Common;

namespace Forma.Domain.Entities;

public class FoodItem : BaseEntity
{ 
    // Per 100g
    public string Name { get; set; } = string.Empty;

    public double Calories { get; set; }
    public double Protein { get; set; }
    public double Carbohydrates { get; set; }
    public double Fats { get; set; }
    public double Fiber { get; set; }
    public double Sugar { get; set; }
    public double Sodium { get; set; }
    public double Cholesterol { get; set; }
    public double Potassium { get; set; }
    public double Calcium { get; set; }
    public double Iron { get; set; }
    public double Magnesium { get; set; }
    public double Zinc { get; set; }

    public double VitaminA { get; set; }
    public double VitaminB6 { get; set; }
    public double VitaminB12 { get; set; }
    public double VitaminC { get; set; }
    public double VitaminD { get; set; }
    public double VitaminE { get; set; }
    public double VitaminK { get; set; }

    public ICollection<MealFoodItem> MealFoodItems { get; set; } = new List<MealFoodItem>();
}

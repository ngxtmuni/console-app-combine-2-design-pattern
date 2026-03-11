using KitchenManagement.ConsoleApp.Enums;

namespace KitchenManagement.ConsoleApp.Models;

public sealed class FoodDish : Dish
{
    public FoodDish(int id, string name, decimal price)
        : base(id, name, price, DishCategory.Food)
    {
    }

    public override string GetStation()
    {
        return "Kitchen";
    }
}

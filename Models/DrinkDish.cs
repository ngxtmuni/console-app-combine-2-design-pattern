using KitchenManagement.ConsoleApp.Enums;

namespace KitchenManagement.ConsoleApp.Models;

public sealed class DrinkDish : Dish
{
    public DrinkDish(int id, string name, decimal price)
        : base(id, name, price, DishCategory.Drink)
    {
    }

    public override string GetStation()
    {
        return "Drink Station";
    }
}

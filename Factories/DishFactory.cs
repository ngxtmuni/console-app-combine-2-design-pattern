using KitchenManagement.ConsoleApp.Enums;
using KitchenManagement.ConsoleApp.Models;

namespace KitchenManagement.ConsoleApp.Factories;

public sealed class DishFactory
{
    public Dish CreateDish(int id, DishCategory category, string name, decimal price)
    {
        return category switch
        {
            DishCategory.Food => new FoodDish(id, name, price),
            DishCategory.Drink => new DrinkDish(id, name, price),
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, "Unsupported dish category.")
        };
    }
}

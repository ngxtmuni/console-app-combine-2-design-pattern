using KitchenManagement.ConsoleApp.Enums;
using KitchenManagement.ConsoleApp.Models;

namespace KitchenManagement.ConsoleApp.Factories;

public sealed class DishFactory
{
    public Dish CreateDish(int id, DishCategory category, string name, decimal price, int preparationTime)
    {
        return category switch
        {
            DishCategory.Appetizer => new Appetizer(id, name, price, preparationTime),
            DishCategory.MainCourse => new MainCourse(id, name, price, preparationTime),
            DishCategory.Dessert => new Dessert(id, name, price, preparationTime),
            DishCategory.Drink => new Drink(id, name, price, preparationTime),
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, "Unsupported dish category.")
        };
    }
}

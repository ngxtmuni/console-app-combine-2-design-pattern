using KitchenManagement.ConsoleApp.Enums;

namespace KitchenManagement.ConsoleApp.Models;

public sealed class Appetizer : Dish
{
    public Appetizer(int id, string name, decimal price, int preparationTime)
        : base(id, name, price, preparationTime, DishCategory.Appetizer)
    {
    }

    public override string GetKitchenNote()
    {
        return "Serve first to start the meal";
    }
}

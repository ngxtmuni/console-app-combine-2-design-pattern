using KitchenManagement.ConsoleApp.Enums;

namespace KitchenManagement.ConsoleApp.Models;

public sealed class Drink : Dish
{
    public Drink(int id, string name, decimal price, int preparationTime)
        : base(id, name, price, preparationTime, DishCategory.Drink)
    {
    }

    public override string GetKitchenNote()
    {
        return "Can be prepared in the beverage corner";
    }
}

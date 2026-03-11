using KitchenManagement.ConsoleApp.Enums;

namespace KitchenManagement.ConsoleApp.Models;

public sealed class Dessert : Dish
{
    public Dessert(int id, string name, decimal price, int preparationTime)
        : base(id, name, price, preparationTime, DishCategory.Dessert)
    {
    }

    public override string GetKitchenNote()
    {
        return "Prepare near the end of the serving flow";
    }
}

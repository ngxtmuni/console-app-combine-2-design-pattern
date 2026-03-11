using KitchenManagement.ConsoleApp.Enums;

namespace KitchenManagement.ConsoleApp.Models;

public sealed class MainCourse : Dish
{
    public MainCourse(int id, string name, decimal price, int preparationTime)
        : base(id, name, price, preparationTime, DishCategory.MainCourse)
    {
    }

    public override string GetKitchenNote()
    {
        return "Core dish that needs station priority";
    }
}

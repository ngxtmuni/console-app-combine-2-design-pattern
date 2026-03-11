using KitchenManagement.ConsoleApp.Enums;

namespace KitchenManagement.ConsoleApp.Models;

public abstract class Dish
{
    protected Dish(int id, string name, decimal price, int preparationTime, DishCategory category)
    {
        Id = id;
        Name = name;
        Price = price;
        PreparationTime = preparationTime;
        Category = category;
    }

    public int Id { get; }

    public string Name { get; }

    public decimal Price { get; }

    public int PreparationTime { get; }

    public DishCategory Category { get; }

    public virtual string GetKitchenNote()
    {
        return "General kitchen item";
    }

    public virtual string ShowInfo()
    {
        return $"[{Id}] {Name} | {Category} | {Price:C} | Prep: {PreparationTime} mins | {GetKitchenNote()}";
    }
}

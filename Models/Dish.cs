using KitchenManagement.ConsoleApp.Enums;

namespace KitchenManagement.ConsoleApp.Models;

public abstract class Dish
{
    protected Dish(int id, string name, decimal price, DishCategory category)
    {
        Id = id;
        Name = name;
        Price = price;
        Category = category;
    }

    public int Id { get; }

    public string Name { get; }

    public decimal Price { get; }

    public DishCategory Category { get; }

    public virtual string GetStation()
    {
        return "General";
    }

    public virtual string ShowInfo()
    {
        return $"[{Id}] {Name} | {Category} | {Price:C} | Station: {GetStation()}";
    }
}

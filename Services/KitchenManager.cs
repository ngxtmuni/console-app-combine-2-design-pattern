using KitchenManagement.ConsoleApp.Enums;
using KitchenManagement.ConsoleApp.Factories;
using KitchenManagement.ConsoleApp.Models;

namespace KitchenManagement.ConsoleApp.Services;

public sealed class KitchenManager
{
    private static readonly Lazy<KitchenManager> LazyInstance = new(() => new KitchenManager());

    private readonly List<Dish> _dishes = [];
    private readonly List<KitchenOrder> _orders = [];
    private readonly DishFactory _dishFactory = new();
    private int _nextDishId = 1;
    private int _nextOrderId = 1;

    private KitchenManager()
    {
        SeedDefaultData();
    }

    public static KitchenManager Instance => LazyInstance.Value;

    public IReadOnlyList<Dish> GetAllDishes()
    {
        return _dishes.AsReadOnly();
    }

    public IReadOnlyList<KitchenOrder> GetAllOrders()
    {
        return _orders.AsReadOnly();
    }

    public Dish AddDish(DishCategory category, string name, decimal price)
    {
        var dish = _dishFactory.CreateDish(_nextDishId++, category, name, price);
        _dishes.Add(dish);
        return dish;
    }

    public Dish? FindDishById(int dishId)
    {
        return _dishes.FirstOrDefault(dish => dish.Id == dishId);
    }

    public KitchenOrder CreateOrder(int dishId)
    {
        var dish = FindDishById(dishId)
            ?? throw new InvalidOperationException($"Dish with id {dishId} was not found.");

        var order = new KitchenOrder(_nextOrderId++, dish);
        _orders.Add(order);
        return order;
    }

    public bool UpdateOrderStatus(int orderId, OrderStatus newStatus)
    {
        var order = _orders.FirstOrDefault(item => item.OrderId == orderId);
        if (order is null)
        {
            return false;
        }

        order.UpdateStatus(newStatus);
        return true;
    }

    private void SeedDefaultData()
    {
        AddDish(DishCategory.Food, "Fried Rice", 4.50m);
        AddDish(DishCategory.Drink, "Lemon Tea", 2.00m);
    }
}

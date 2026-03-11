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
        return _orders
            .OrderBy(order => order.OrderId)
            .ToList()
            .AsReadOnly();
    }

    public Dish AddDish(DishCategory category, string name, decimal price, int preparationTime)
    {
        var dish = _dishFactory.CreateDish(_nextDishId++, category, name, price, preparationTime);
        _dishes.Add(dish);
        return dish;
    }

    public Dish? FindDishById(int dishId)
    {
        return _dishes.FirstOrDefault(dish => dish.Id == dishId);
    }

    public KitchenOrder CreateOrder(IEnumerable<(int dishId, int quantity)> requests)
    {
        var items = new List<OrderItem>();

        foreach (var request in requests)
        {
            var dish = FindDishById(request.dishId)
                ?? throw new InvalidOperationException($"Dish with id {request.dishId} was not found.");

            if (request.quantity <= 0)
            {
                throw new InvalidOperationException("Quantity must be greater than zero.");
            }

            items.Add(new OrderItem(dish, request.quantity));
        }

        if (items.Count == 0)
        {
            throw new InvalidOperationException("Order must contain at least one dish.");
        }

        var order = new KitchenOrder(_nextOrderId++, items);
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

    public IReadOnlyList<KitchenOrder> GetOrdersByStatus(OrderStatus status)
    {
        return _orders
            .Where(order => order.Status == status)
            .OrderBy(order => order.OrderId)
            .ToList()
            .AsReadOnly();
    }

    public Dictionary<OrderStatus, int> GetDashboardSummary()
    {
        return Enum.GetValues<OrderStatus>()
            .ToDictionary(status => status, status => _orders.Count(order => order.Status == status));
    }

    private void SeedDefaultData()
    {
        AddDish(DishCategory.Appetizer, "Spring Roll", 4.50m, 8);
        AddDish(DishCategory.MainCourse, "Beef Steak", 14.99m, 18);
        AddDish(DishCategory.Drink, "Orange Juice", 3.20m, 3);
        AddDish(DishCategory.Dessert, "Cheesecake", 5.75m, 6);
    }
}

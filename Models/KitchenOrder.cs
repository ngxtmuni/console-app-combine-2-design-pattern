using KitchenManagement.ConsoleApp.Enums;

namespace KitchenManagement.ConsoleApp.Models;

public sealed class KitchenOrder
{
    public KitchenOrder(int orderId, Dish dish)
    {
        OrderId = orderId;
        Dish = dish;
        Status = OrderStatus.Pending;
    }

    public int OrderId { get; }

    public Dish Dish { get; }

    public OrderStatus Status { get; private set; }

    public void UpdateStatus(OrderStatus newStatus)
    {
        Status = newStatus;
    }

    public string ShowInfo()
    {
        return $"Order #{OrderId} | {Dish.Name} | {Status} | {Dish.Price:C}";
    }
}

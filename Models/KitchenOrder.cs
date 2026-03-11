using KitchenManagement.ConsoleApp.Enums;

namespace KitchenManagement.ConsoleApp.Models;

public sealed class KitchenOrder
{
    public KitchenOrder(int orderId, IEnumerable<OrderItem> items)
    {
        OrderId = orderId;
        CreatedAt = DateTime.Now;
        Status = OrderStatus.Pending;
        Items = items.ToList().AsReadOnly();
    }

    public int OrderId { get; }

    public DateTime CreatedAt { get; }

    public OrderStatus Status { get; private set; }

    public IReadOnlyList<OrderItem> Items { get; }

    public decimal TotalAmount => Items.Sum(item => item.SubTotal);

    public int EstimatedPreparationTime => Items.Sum(item => item.Dish.PreparationTime * item.Quantity);

    public void UpdateStatus(OrderStatus newStatus)
    {
        Status = newStatus;
    }

    public string ShowSummary()
    {
        return $"Order #{OrderId} | {Status} | {CreatedAt:dd/MM/yyyy HH:mm} | Items: {Items.Count} | Total: {TotalAmount:C}";
    }
}

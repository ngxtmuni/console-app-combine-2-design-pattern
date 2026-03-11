namespace KitchenManagement.ConsoleApp.Models;

public sealed class OrderItem
{
    public OrderItem(Dish dish, int quantity)
    {
        Dish = dish;
        Quantity = quantity;
    }

    public Dish Dish { get; }

    public int Quantity { get; }

    public decimal SubTotal => Dish.Price * Quantity;

    public string ShowInfo()
    {
        return $"{Dish.Name} x{Quantity} = {SubTotal:C}";
    }
}

using KitchenManagement.ConsoleApp.Enums;
using KitchenManagement.ConsoleApp.Models;
using KitchenManagement.ConsoleApp.Services;

namespace KitchenManagement.ConsoleApp.UI;

public sealed class ConsoleMenu
{
    private readonly KitchenManager _kitchenManager = KitchenManager.Instance;

    public void Run()
    {
        var isRunning = true;

        while (isRunning)
        {
            TryClearConsole();
            PrintHeader();
            PrintMainMenu();

            var choice = ReadInt("Choose an option: ");
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    ShowAllDishes();
                    break;
                case 2:
                    AddNewDish();
                    break;
                case 3:
                    CreateNewOrder();
                    break;
                case 4:
                    ShowAllOrders();
                    break;
                case 5:
                    UpdateOrderStatus();
                    break;
                case 6:
                    ShowDashboard();
                    break;
                case 0:
                    isRunning = false;
                    break;
                default:
                    WriteWarning("Invalid menu option.");
                    Pause();
                    break;
            }
        }

        Console.WriteLine("Kitchen management app closed.");
    }

    private static void TryClearConsole()
    {
        try
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }
        }
        catch (IOException)
        {
        }
    }

    private static void PrintHeader()
    {
        Console.WriteLine("==============================================");
        Console.WriteLine("      KITCHEN MANAGEMENT CONSOLE SYSTEM       ");
        Console.WriteLine("==============================================");
        Console.WriteLine("Design Patterns: Singleton + Factory");
        Console.WriteLine();
    }

    private static void PrintMainMenu()
    {
        Console.WriteLine("1. View all dishes");
        Console.WriteLine("2. Add new dish");
        Console.WriteLine("3. Create new order");
        Console.WriteLine("4. View all orders");
        Console.WriteLine("5. Update order status");
        Console.WriteLine("6. View kitchen dashboard");
        Console.WriteLine("0. Exit");
    }

    private void ShowAllDishes()
    {
        Console.WriteLine("Dish List");
        Console.WriteLine("----------------------------------------------");

        foreach (var dish in _kitchenManager.GetAllDishes())
        {
            Console.WriteLine(dish.ShowInfo());
        }

        Pause();
    }

    private void AddNewDish()
    {
        Console.WriteLine("Add New Dish");
        Console.WriteLine("----------------------------------------------");

        var category = ReadDishCategory();
        var name = ReadRequiredString("Dish name: ");
        var price = ReadDecimal("Price: ");
        var preparationTime = ReadInt("Preparation time (minutes): ", value => value > 0);

        var dish = _kitchenManager.AddDish(category, name, price, preparationTime);

        Console.WriteLine();
        Console.WriteLine("Dish created successfully.");
        Console.WriteLine(dish.ShowInfo());
        Pause();
    }

    private void CreateNewOrder()
    {
        Console.WriteLine("Create New Order");
        Console.WriteLine("----------------------------------------------");
        ShowAllDishesInline();
        Console.WriteLine();
        Console.WriteLine("Enter dish id and quantity. Type 0 to finish.");

        var requests = new List<(int dishId, int quantity)>();

        while (true)
        {
            var dishId = ReadInt("Dish id: ", value => value >= 0);
            if (dishId == 0)
            {
                break;
            }

            var dish = _kitchenManager.FindDishById(dishId);
            if (dish is null)
            {
                WriteWarning("Dish id does not exist.");
                continue;
            }

            var quantity = ReadInt("Quantity: ", value => value > 0);
            requests.Add((dishId, quantity));

            Console.WriteLine("Item added to pending order.");
        }

        try
        {
            var order = _kitchenManager.CreateOrder(requests);
            PrintOrderDetails(order);
        }
        catch (InvalidOperationException exception)
        {
            WriteWarning(exception.Message);
        }

        Pause();
    }

    private void ShowAllOrders()
    {
        Console.WriteLine("All Orders");
        Console.WriteLine("----------------------------------------------");

        var orders = _kitchenManager.GetAllOrders();
        if (orders.Count == 0)
        {
            WriteWarning("No orders found.");
            Pause();
            return;
        }

        foreach (var order in orders)
        {
            PrintOrderDetails(order);
            Console.WriteLine();
        }

        Pause();
    }

    private void UpdateOrderStatus()
    {
        Console.WriteLine("Update Order Status");
        Console.WriteLine("----------------------------------------------");

        var orders = _kitchenManager.GetAllOrders();
        if (orders.Count == 0)
        {
            WriteWarning("No orders available to update.");
            Pause();
            return;
        }

        foreach (var order in orders)
        {
            Console.WriteLine(order.ShowSummary());
        }

        Console.WriteLine();
        var orderId = ReadInt("Order id: ", value => value > 0);
        var newStatus = ReadOrderStatus();

        var updated = _kitchenManager.UpdateOrderStatus(orderId, newStatus);
        Console.WriteLine(updated
            ? "Order status updated successfully."
            : "Order id was not found.");

        Pause();
    }

    private void ShowDashboard()
    {
        Console.WriteLine("Kitchen Dashboard");
        Console.WriteLine("----------------------------------------------");

        var summary = _kitchenManager.GetDashboardSummary();
        foreach (var entry in summary)
        {
            Console.WriteLine($"{entry.Key,-12}: {entry.Value}");
        }

        var orders = _kitchenManager.GetAllOrders();
        Console.WriteLine();
        Console.WriteLine($"Total orders: {orders.Count}");

        if (orders.Count > 0)
        {
            Console.WriteLine($"Revenue estimate: {orders.Sum(order => order.TotalAmount):C}");
            Console.WriteLine($"Total prep time estimate: {orders.Sum(order => order.EstimatedPreparationTime)} mins");
        }

        Pause();
    }

    private static void PrintOrderDetails(KitchenOrder order)
    {
        Console.WriteLine(order.ShowSummary());
        foreach (var item in order.Items)
        {
            Console.WriteLine($"  - {item.ShowInfo()}");
        }

        Console.WriteLine($"  Estimated prep time: {order.EstimatedPreparationTime} mins");
    }

    private void ShowAllDishesInline()
    {
        foreach (var dish in _kitchenManager.GetAllDishes())
        {
            Console.WriteLine(dish.ShowInfo());
        }
    }

    private static DishCategory ReadDishCategory()
    {
        Console.WriteLine("Dish categories:");
        foreach (var category in Enum.GetValues<DishCategory>())
        {
            Console.WriteLine($"{(int)category}. {category}");
        }

        var value = ReadInt("Category: ", raw => Enum.IsDefined(typeof(DishCategory), raw));
        return (DishCategory)value;
    }

    private static OrderStatus ReadOrderStatus()
    {
        Console.WriteLine("Available statuses:");
        foreach (var status in Enum.GetValues<OrderStatus>())
        {
            Console.WriteLine($"{(int)status}. {status}");
        }

        var value = ReadInt("New status: ", raw => Enum.IsDefined(typeof(OrderStatus), raw));
        return (OrderStatus)value;
    }

    private static int ReadInt(string prompt, Func<int, bool>? validator = null)
    {
        while (true)
        {
            Console.Write(prompt);
            var raw = Console.ReadLine();

            if (int.TryParse(raw, out var value) && (validator is null || validator(value)))
            {
                return value;
            }

            WriteWarning("Please enter a valid integer value.");
        }
    }

    private static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var raw = Console.ReadLine();

            if (decimal.TryParse(raw, out var value) && value > 0)
            {
                return value;
            }

            WriteWarning("Please enter a valid positive decimal value.");
        }
    }

    private static string ReadRequiredString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var raw = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(raw))
            {
                return raw.Trim();
            }

            WriteWarning("This field is required.");
        }
    }

    private static void Pause()
    {
        Console.WriteLine();
        Console.Write("Press Enter to continue...");
        Console.ReadLine();
    }

    private static void WriteWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

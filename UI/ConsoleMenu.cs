using KitchenManagement.ConsoleApp.Enums;
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
                    UpdateOrderStatus();
                    break;
                case 5:
                    ShowAllOrders();
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
        Console.WriteLine("4. Update order status");
        Console.WriteLine("5. View all orders");
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

        var dish = _kitchenManager.AddDish(category, name, price);

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
        var dishId = ReadInt("Choose dish id: ", value => value > 0);

        try
        {
            var order = _kitchenManager.CreateOrder(dishId);
            Console.WriteLine(order.ShowInfo());
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
            Console.WriteLine(order.ShowInfo());
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
            Console.WriteLine(order.ShowInfo());
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

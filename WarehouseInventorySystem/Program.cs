using System;
using WarehouseInventorySystem.Models;
using WarehouseInventorySystem.Repositories;
using WarehouseInventorySystem.Exceptions;
using WarehouseInventorySystem.Interfaces;

namespace WarehouseInventorySystem
{
    public class WareHouseManager
    {
        private InventoryRepository<ElectronicItem> _electronics = new InventoryRepository<ElectronicItem>();
        private InventoryRepository<GroceryItem> _groceries = new InventoryRepository<GroceryItem>();

        public void SeedData()
        {
            try
            {
                Console.WriteLine("Initializing warehouse inventory...\n");
                
                // Electronics inventory
                _electronics.AddItem(new ElectronicItem(1001, "Gaming Laptop", 8, "ASUS ROG", 36));
                _electronics.AddItem(new ElectronicItem(1002, "Wireless Earbuds", 25, "Apple AirPods", 12));
                _electronics.AddItem(new ElectronicItem(1003, "Smart Watch", 12, "Samsung Galaxy", 24));

                // Grocery inventory
                _groceries.AddItem(new GroceryItem(2001, "Organic Milk", 45, DateTime.Now.AddDays(10)));
                _groceries.AddItem(new GroceryItem(2002, "Whole Grain Bread", 35, DateTime.Now.AddDays(5)));
                _groceries.AddItem(new GroceryItem(2003, "Free Range Eggs", 55, DateTime.Now.AddDays(21)));
                
                Console.WriteLine("Warehouse inventory initialized successfully!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing inventory: {ex.Message}");
            }
        }

        public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            var items = repo.GetAllItems();
            if (items.Count == 0)
            {
                Console.WriteLine("No items found in inventory.");
                return;
            }

            string categoryName = typeof(T) == typeof(ElectronicItem) ? "Electronics" : "Groceries";
            
            Console.WriteLine($"{categoryName} Inventory:");
            Console.WriteLine(new string('-', 70));

            foreach (var item in items)
            {
                if (item is ElectronicItem e)
                    Console.WriteLine($"#{e.Id} | {e.Name} | Stock: {e.Quantity} | Brand: {e.Brand} | Warranty: {e.WarrantyMonths}mo");
                else if (item is GroceryItem g)
                    Console.WriteLine($"#{g.Id} | {g.Name} | Stock: {g.Quantity} | Expires: {g.ExpiryDate:MMM dd, yyyy}");
            }
            Console.WriteLine();
        }

        public void IncreaseStock<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            Console.Write("Enter Item ID to increase stock: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); return; }

            Console.Write("Enter quantity to add: ");
            if (!int.TryParse(Console.ReadLine(), out int qty)) { Console.WriteLine("Invalid quantity."); return; }

            try
            {
                var item = repo.GetItemById(id);
                repo.UpdateQuantity(id, item.Quantity + qty);
                Console.WriteLine($"Stock updated. {item.Name} now has quantity {item.Quantity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void RemoveItemById<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            Console.Write("Enter Item ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid ID."); return; }

            try
            {
                repo.RemoveItem(id);
                Console.WriteLine($"Item with ID {id} removed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void AddNewItem()
        {
            Console.WriteLine("Select item type to add:\n1. Electronic\n2. Grocery");
            var choice = Console.ReadLine();
            if (choice == "1")
            {
                try
                {
                    Console.Write("Enter ID: "); int id = int.Parse(Console.ReadLine());
                    Console.Write("Enter Name: "); string name = Console.ReadLine();
                    Console.Write("Enter Quantity: "); int qty = int.Parse(Console.ReadLine());
                    Console.Write("Enter Brand: "); string brand = Console.ReadLine();
                    Console.Write("Enter Warranty Months: "); int warranty = int.Parse(Console.ReadLine());

                    _electronics.AddItem(new ElectronicItem(id, name, qty, brand, warranty));
                    Console.WriteLine("Electronic item added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else if (choice == "2")
            {
                try
                {
                    Console.Write("Enter ID: "); int id = int.Parse(Console.ReadLine());
                    Console.Write("Enter Name: "); string name = Console.ReadLine();
                    Console.Write("Enter Quantity: "); int qty = int.Parse(Console.ReadLine());
                    Console.Write("Enter Expiry Date (yyyy-mm-dd): "); DateTime expiry = DateTime.Parse(Console.ReadLine());

                    _groceries.AddItem(new GroceryItem(id, name, qty, expiry));
                    Console.WriteLine("Grocery item added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        public void RunTestScenario()
        {
            Console.WriteLine("\n--- Running Test Scenario ---");

            // Add duplicate item
            try
            {
                _electronics.AddItem(new ElectronicItem(1, "Tablet", 5, "Apple", 12));
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($"Duplicate Error: {ex.Message}");
            }

            // Remove non-existent item
            try
            {
                _groceries.RemoveItem(999);
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Remove Error: {ex.Message}");
            }

            // Update with invalid quantity
            try
            {
                _groceries.UpdateQuantity(101, -10);
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"Invalid Quantity Error: {ex.Message}");
            }

            Console.WriteLine("--- Test Scenario Completed ---");
        }

        public void Run()
        {
            SeedData();
            while (true)
            {
                Console.WriteLine("\n--- Warehouse Inventory Menu ---");
                Console.WriteLine("1. Print All Grocery Items");
                Console.WriteLine("2. Print All Electronic Items");
                Console.WriteLine("3. Add New Item");
                Console.WriteLine("4. Increase Stock");
                Console.WriteLine("5. Remove Item");
                Console.WriteLine("6. Exit");
                Console.WriteLine("7. Run Test Scenario (Duplicate / Non-existent / Invalid Quantity)");
                Console.Write("Enter choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PrintAllItems(_groceries);
                        break;
                    case "2":
                        PrintAllItems(_electronics);
                        break;
                    case "3":
                        AddNewItem();
                        break;
                    case "4":
                        Console.WriteLine("Select inventory:\n1. Grocery\n2. Electronic");
                        var invChoice = Console.ReadLine();
                        if (invChoice == "1") IncreaseStock(_groceries);
                        else if (invChoice == "2") IncreaseStock(_electronics);
                        else Console.WriteLine("Invalid inventory choice.");
                        break;
                    case "5":
                        Console.WriteLine("Select inventory:\n1. Grocery\n2. Electronic");
                        var remChoice = Console.ReadLine();
                        if (remChoice == "1") RemoveItemById(_groceries);
                        else if (remChoice == "2") RemoveItemById(_electronics);
                        else Console.WriteLine("Invalid inventory choice.");
                        break;
                    case "6":
                        Console.WriteLine("Exiting...");
                        return;
                    case "7":
                        RunTestScenario();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        public static void Main()
        {
            WareHouseManager manager = new WareHouseManager();
            manager.Run();
        }
    }
}

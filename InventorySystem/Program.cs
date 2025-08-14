using System;

namespace InventorySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Smart Inventory Management System");
            Console.WriteLine(new string('=', 40));
            
            string filePath = "warehouse_inventory.json";

            // Initialize inventory system
            Console.WriteLine("Initializing inventory system...");
            InventoryApp app = new InventoryApp(filePath);

            // Populate with sample inventory data
            Console.WriteLine("Adding sample inventory items...");
            app.SeedSampleData();

            // Persist data to storage
            Console.WriteLine("Saving inventory to persistent storage...");
            app.SaveData();

            // Simulate system restart
            Console.WriteLine("\nSimulating system restart...");
            Console.WriteLine(new string('-', 40));
            app = new InventoryApp(filePath);

            // Restore data from storage
            Console.WriteLine("Loading inventory from storage...");
            app.LoadData();

            // Display current inventory
            Console.WriteLine("Current Inventory Status:");
            app.PrintAllItems();
        }
    }
}

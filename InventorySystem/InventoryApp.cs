using System;

namespace InventorySystem
{
    public class InventoryApp
    {
        private InventoryLogger<InventoryItem> _logger;

        public InventoryApp(string filePath)
        {
            _logger = new InventoryLogger<InventoryItem>(filePath);
        }

        public void SeedSampleData()
        {
            // Pre-populate with sample data instead of interactive input
            var sampleItems = new[]
            {
                new InventoryItem(3001, "Wireless Mouse", 45, DateTime.Now.AddDays(-5)),
                new InventoryItem(3002, "USB-C Cable", 120, DateTime.Now.AddDays(-3)),
                new InventoryItem(3003, "Bluetooth Speaker", 28, DateTime.Now.AddDays(-2)),
                new InventoryItem(3004, "Phone Charger", 85, DateTime.Now.AddDays(-1)),
                new InventoryItem(3005, "Laptop Stand", 15, DateTime.Now)
            };

            foreach (var item in sampleItems)
            {
                _logger.Add(item);
            }
            
            Console.WriteLine($"Added {sampleItems.Length} sample items to inventory");
        }

        public void SaveData()
        {
            _logger.SaveToFile();
        }

        public void LoadData()
        {
            _logger.LoadFromFile();
        }

        public void PrintAllItems()
        {
            var items = _logger.GetAll();
            Console.WriteLine($"\nInventory Summary ({items.Count} items):");
            Console.WriteLine(new string('=', 65));
            Console.WriteLine($"{"Item ID",-8} {"Product Name",-20} {"Stock",-8} {"Added On",-15}");
            Console.WriteLine(new string('-', 65));
            
            foreach (var item in items)
            {
                string stockStatus = item.Quantity < 20 ? "Low" : "OK";
                Console.WriteLine($"{item.Id,-8} {item.Name,-20} {item.Quantity,-8} {item.DateAdded:MMM dd, yyyy}");
            }
            Console.WriteLine(new string('=', 65));
        }
    }
}

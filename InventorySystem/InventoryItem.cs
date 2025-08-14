using System;

namespace InventorySystem
{
    // Marker interface implementation
    public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;
}

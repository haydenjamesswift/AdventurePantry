using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;

namespace AP.Plugins.InMemory;

public class InventoryRepository : IInventoryRepository
{
    // Attributes
    private List<Inventory> _inventories;

    // Constructor
    public InventoryRepository()
    {
        _inventories = new List<Inventory>()
        {
            new Inventory { InventoryId = 1, InventoryName = "Bike Seat", Quantity = 100, Price = 15},
            new Inventory { InventoryId = 2, InventoryName = "Bike Frame", Quantity = 100, Price = 25},
            new Inventory { InventoryId = 3, InventoryName = "Bike Pedals", Quantity = 200, Price = 10},
            new Inventory { InventoryId = 4, InventoryName = "Bike Wheels", Quantity = 200, Price = 10}
        };
    }

    public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
    {

        if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_inventories);

        return _inventories.Where(x => x.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public Task AddInventoryAsync(Inventory inventory)
    {
        // Validate input
        if (inventory.InventoryName is null || inventory.Quantity < 0 || inventory.Price < 0)
        {
            return Task.CompletedTask;
        }

        // Generate new InventoryId
        var maxId = _inventories.Max(x => x.InventoryId);
        inventory.InventoryId = maxId + 1;

        // Add inventory
        _inventories.Add(inventory);

        return Task.CompletedTask;
    }

    public Task UpdateInventoryAsync(Inventory inventory)
    {

        if (_inventories.Any(x => x.InventoryId != inventory.InventoryId && x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
        {
            return Task.CompletedTask;
        }

        var prodToUpdate = _inventories.FirstOrDefault(x => x.InventoryId == inventory.InventoryId);

        if (prodToUpdate is not null)
        {
            prodToUpdate.InventoryName = inventory.InventoryName;
            prodToUpdate.Quantity = inventory.Quantity;
            prodToUpdate.Price = inventory.Price;
        }

        return Task.CompletedTask;
    }

    public async Task<Inventory> FetchInventoryByIdAsync(int inventoryId)
    {
        return await Task.FromResult(_inventories.First(x => x.InventoryId == inventoryId));
    }

    public Task DeleteInventoryAsync(int inventoryId)
    {
        // Check that inventory exists
        var inventoryToDelete = _inventories.FirstOrDefault(x => x.InventoryId == inventoryId);

        if (inventoryToDelete is not null)
        {
            _inventories.Remove(inventoryToDelete);
        }

        return Task.CompletedTask;
    }

}
 
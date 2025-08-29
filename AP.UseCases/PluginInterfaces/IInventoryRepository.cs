using System;
using AP.CoreBusiness;

namespace AP.UseCases.PluginInterfaces;

public interface IInventoryRepository
{
    Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name);
    Task AddInventoryAsync(Inventory inventory);
    Task UpdateInventoryAsync(Inventory inventory);
    Task<Inventory> FetchInventoryByIdAsync(int inventoryId);

    Task DeleteInventoryAsync(int inventoryId);
}

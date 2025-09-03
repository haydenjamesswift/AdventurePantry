using System;
using System.Runtime.Versioning;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;

namespace AP.Plugins.InMemory;

public class InventoryTransactionRepository : IInventoryTransactionRepository
{
    // Attributes
    private List<InventoryTransaction> _inventoryTransactions = new();
    private IInventoryRepository inventoryRepository;

    public InventoryTransactionRepository(IInventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public void PurchaseAsync(string poNumber, Inventory inventory, int quantity, double price, string doneBy)
    {
        this._inventoryTransactions.Add(new InventoryTransaction
        {
            PONumber = poNumber,
            InventoryId = inventory.InventoryId,
            QuantityBefore = inventory.Quantity,
            QuantityAfter = inventory.Quantity + quantity,
            ActivityType = InventoryTransactionType.PurchaseInventory,
            UnitPrice = price,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
        });
    }

    public void ProduceAsync(string productionNumber, Inventory inventory, int quantityToConsume, double price, string doneBy)
    {
        this._inventoryTransactions.Add(new InventoryTransaction
        {
            ProductionNumber = productionNumber,
            InventoryId = inventory.InventoryId,
            QuantityBefore = inventory.Quantity,
            QuantityAfter = inventory.Quantity - quantityToConsume,
            ActivityType = InventoryTransactionType.ProduceProduct,
            UnitPrice = price,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
        });
    }

    public async Task<IEnumerable<InventoryTransaction>> SearchInventoryTransactionsAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType)
    {
        // Get all inventories by inventory name
        var inventories = (await this.inventoryRepository.GetInventoriesByNameAsync(string.Empty)).ToList();

        var query = from it in this._inventoryTransactions
                    join inv in inventories on it.InventoryId equals inv.InventoryId
                    where
                        (string.IsNullOrWhiteSpace(inventoryName) || inv.InventoryName.ToLower().IndexOf(inventoryName.ToLower()) >= 0) && (!dateFrom.HasValue || it.TransactionDate >= dateFrom.Value.Date) && (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date) && (!transactionType.HasValue || it.ActivityType == transactionType.Value)
                    select new InventoryTransaction
                    {
                        Inventory = inv,
                        InventoryTransactionId = it.InventoryTransactionId,
                        PONumber = it.PONumber,
                        ProductionNumber = it.ProductionNumber,
                        InventoryId = it.InventoryId,
                        QuantityBefore = it.QuantityBefore,
                        QuantityAfter = it.QuantityAfter,
                        ActivityType = it.ActivityType,
                        TransactionDate = it.TransactionDate,
                        DoneBy = it.DoneBy,
                        UnitPrice = it.UnitPrice
                    };

        return query;
    }
}

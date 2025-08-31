using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;

namespace AP.Plugins.InMemory;

public class InventoryTransactionRepository : IInventoryTransactionRepository
{
    // Attributes
    private List<InventoryTransaction> _inventoryTransactions = new();

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
}

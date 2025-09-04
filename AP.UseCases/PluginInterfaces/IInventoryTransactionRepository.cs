using System;
using AP.CoreBusiness;

namespace AP.UseCases.PluginInterfaces;

public interface IInventoryTransactionRepository
{
    Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, double price, string doneBy);

    Task ProduceAsync(string productionNumber, Inventory inventory, int quantity, double price, string doneBy);

    Task<IEnumerable<InventoryTransaction>> SearchInventoryTransactionsAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType);

}

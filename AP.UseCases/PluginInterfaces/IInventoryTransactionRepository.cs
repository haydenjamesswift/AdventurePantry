using System;
using AP.CoreBusiness;

namespace AP.UseCases.PluginInterfaces;

public interface IInventoryTransactionRepository
{
    void PurchaseAsync(string poNumber, Inventory inventory, int quantity, double price, string doneBy);
}

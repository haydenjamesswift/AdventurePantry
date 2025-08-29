using System;
using AP.CoreBusiness;
using AP.UseCases.Activities.Interfaces;
using AP.UseCases.PluginInterfaces;

namespace AP.UseCases.Activities;

public class PurchaseInventoryUseCase : IPurchaseInventoryUseCase
{
    private readonly IInventoryTransactionRepository inventoryTransactionRepository;
    private readonly IInventoryRepository inventoryRepository;

    public PurchaseInventoryUseCase(IInventoryTransactionRepository inventoryTransactionRepository, IInventoryRepository inventoryRepository)
    {
        this.inventoryTransactionRepository = inventoryTransactionRepository;
        this.inventoryRepository = inventoryRepository;
    }

    public async Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, double price, string doneBy)
    {
        // Insert record in transaction table
        inventoryTransactionRepository.PurchaseAsync(poNumber, inventory, quantity, inventory.Price, doneBy);

        // Increase quantity in inventory
        inventory.Quantity += quantity;
        await this.inventoryRepository.UpdateInventoryAsync(inventory);
    }
}

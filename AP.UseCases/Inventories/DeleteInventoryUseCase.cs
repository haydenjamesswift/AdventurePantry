using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Inventories.Interfaces;

namespace AP.UseCases.Inventories;

public class DeleteInventoryUseCase : IDeleteInventoryUseCase
{
    // Attributes
    private readonly IInventoryRepository inventoryRepository;

    // Constructor
    public DeleteInventoryUseCase(IInventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    // Function
    public async Task ExecuteAsync(int inventoryId)
    {
        await this.inventoryRepository.DeleteInventoryAsync(inventoryId);
    }
}

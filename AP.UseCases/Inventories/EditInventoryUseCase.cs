using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Inventories.Interfaces;

namespace AP.UseCases.Inventories;

public class EditInventoryUseCase : IEditInventoryUseCase
{
    private readonly IInventoryRepository inventoryRepository;

    public EditInventoryUseCase(IInventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public async Task ExecuteAsync(Inventory inventory)
    {
        await this.inventoryRepository.UpdateInventoryAsync(inventory);
    }
}
 
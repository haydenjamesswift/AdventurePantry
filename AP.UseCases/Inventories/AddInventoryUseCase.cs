using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Inventories;

public class AddInventoryUseCase : IAddInventoryUseCase
{
    private readonly IInventoryRepository inventoryRepository;

    public AddInventoryUseCase(IInventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public async Task ExecuteAsync(Inventory inventory)
    {
        await this.inventoryRepository.AddInventoryAsync(inventory);
    }
}

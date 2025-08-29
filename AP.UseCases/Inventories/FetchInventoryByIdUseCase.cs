using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Inventories.Interfaces;

namespace AP.UseCases.Inventories;

public class FetchInventoryByIdUseCase : IFetchInventoryByIdUseCase
{

    private readonly IInventoryRepository inventoryRepository;

    public FetchInventoryByIdUseCase(IInventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public async Task<Inventory> ExecuteAsync(int inventoryId)
    {
        return await inventoryRepository.FetchInventoryByIdAsync(inventoryId);
    }
}

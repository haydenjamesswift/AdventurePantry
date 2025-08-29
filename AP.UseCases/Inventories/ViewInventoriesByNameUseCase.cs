using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Inventories.Interfaces;

namespace AP.UseCases;

public class ViewInventoriesByNameUseCase : IViewInventoriesByNameUseCase
{
    private readonly IInventoryRepository inventoryRepository;

    public ViewInventoriesByNameUseCase(IInventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public async Task<IEnumerable<Inventory>> ExecuteAsync(string name = "")
    {
        return await inventoryRepository.GetInventoriesByNameAsync(name);
    }
}

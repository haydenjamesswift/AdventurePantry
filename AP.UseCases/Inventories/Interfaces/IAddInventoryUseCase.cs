using AP.CoreBusiness;

namespace AP.UseCases.Inventories;

public interface IAddInventoryUseCase
{
    Task ExecuteAsync(Inventory inventory);
}

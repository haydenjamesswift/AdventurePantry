using System;
using AP.CoreBusiness;

namespace AP.UseCases.Inventories.Interfaces;

public interface IFetchInventoryByIdUseCase
{
    Task<Inventory> ExecuteAsync(int productId);
}

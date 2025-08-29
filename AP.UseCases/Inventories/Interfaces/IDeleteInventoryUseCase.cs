using System;
using AP.CoreBusiness;

namespace AP.UseCases.Inventories.Interfaces;

public interface IDeleteInventoryUseCase
{
    Task ExecuteAsync(int inventoryId);
}


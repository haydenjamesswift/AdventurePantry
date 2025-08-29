using System;
using AP.CoreBusiness;

namespace AP.UseCases.Inventories.Interfaces;
public interface IEditInventoryUseCase
{
    Task ExecuteAsync(Inventory inventory);
}


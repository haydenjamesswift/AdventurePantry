using System;
using AP.CoreBusiness;

namespace AP.UseCases.Inventories.Interfaces;

public interface IViewInventoriesByNameUseCase
{
    Task<IEnumerable<Inventory>> ExecuteAsync(string name = "");
}

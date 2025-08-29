using System;
using AP.CoreBusiness;

namespace AP.UseCases.Activities.Interfaces;

public interface IPurchaseInventoryUseCase
{
    Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, double price, string doneBy);
}

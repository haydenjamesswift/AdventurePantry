using System;
using AP.CoreBusiness;

namespace AP.UseCases.PluginInterfaces;

public interface IProductTransactionRepository
{
    Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy);

}

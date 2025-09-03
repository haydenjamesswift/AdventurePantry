using System;
using AP.CoreBusiness;

namespace AP.UseCases.PluginInterfaces;

public interface IProductTransactionRepository
{
    Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy);
    Task SellProductAsync(string salesOrderNumber, Product product, int quantity, string doneBy);
    Task<IEnumerable<ProductTransaction>> SearchProductTransactionsAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType);

}

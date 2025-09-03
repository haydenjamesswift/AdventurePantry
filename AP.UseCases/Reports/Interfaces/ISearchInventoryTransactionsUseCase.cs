using System;
using AP.CoreBusiness;

namespace AP.UseCases.Reports.Interfaces;

public interface ISearchInventoryTransactionsUseCase
{
    Task<IEnumerable<InventoryTransaction>> ExecuteAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType);
}

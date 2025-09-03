using System;
using AP.CoreBusiness;

namespace AP.UseCases.Reports.Interfaces;

public interface ISearchProductTransactionsUseCase
{
    Task<IEnumerable<ProductTransaction>> ExecuteAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType);
}

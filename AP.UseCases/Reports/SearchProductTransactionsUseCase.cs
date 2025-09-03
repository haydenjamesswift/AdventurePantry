using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Reports.Interfaces;

namespace AP.UseCases.Reports;

public class SearchProductTransactionsUseCase : ISearchProductTransactionsUseCase
{

    private readonly IProductTransactionRepository productTransactionRepository;

    public SearchProductTransactionsUseCase(IProductTransactionRepository productTransactionRepository)
    {
        this.productTransactionRepository = productTransactionRepository;
    }


    public async Task<IEnumerable<ProductTransaction>> ExecuteAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
    {
        // Convert dateTo
        if (dateTo.HasValue)
        {
            dateTo = dateTo.Value.AddDays(1);
        }

    
        return await this.productTransactionRepository.SearchProductTransactionsAsync(productName, dateFrom, dateTo, transactionType);
    }
}
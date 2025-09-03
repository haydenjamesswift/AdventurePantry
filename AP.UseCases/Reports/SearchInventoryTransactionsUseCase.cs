using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Reports.Interfaces;

namespace AP.UseCases.Reports;

public class SearchInventoryTransactionsUseCase : ISearchInventoryTransactionsUseCase
{

    private readonly IInventoryTransactionRepository inventoryTransactionRepository;

    public SearchInventoryTransactionsUseCase(IInventoryTransactionRepository inventoryTransactionRepository)
    {
        this.inventoryTransactionRepository = inventoryTransactionRepository;
    }


    public async Task<IEnumerable<InventoryTransaction>> ExecuteAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType)
    {
        // Convert dateTo
        if (dateTo.HasValue)
        {
            dateTo = dateTo.Value.AddDays(1);
        }

    
        return await this.inventoryTransactionRepository.SearchInventoryTransactionsAsync(inventoryName, dateFrom, dateTo, transactionType);
    }
}

using System;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AP.Plugins.EFCoreSQL;

public class InventoryTransactionEFCoreRepository : IInventoryTransactionRepository
{
    private readonly IDbContextFactory<AppDbContext> contextFactory;

    public InventoryTransactionEFCoreRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public async Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, double price, string doneBy)
    {

        using var db = this.contextFactory.CreateDbContext();

        db.InventoryTransactions?.Add(new InventoryTransaction
        {
            PONumber = poNumber,
            InventoryId = inventory.InventoryId,
            QuantityBefore = inventory.Quantity,
            QuantityAfter = inventory.Quantity + quantity,
            ActivityType = InventoryTransactionType.PurchaseInventory,
            UnitPrice = price,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
        });

        await db.SaveChangesAsync();
    }

    public async Task ProduceAsync(string productionNumber, Inventory inventory, int quantityToConsume, double price, string doneBy)
    {
        using var db = this.contextFactory.CreateDbContext();

        db.InventoryTransactions?.Add(new InventoryTransaction
        {
            ProductionNumber = productionNumber,
            InventoryId = inventory.InventoryId,
            QuantityBefore = inventory.Quantity,
            QuantityAfter = inventory.Quantity - quantityToConsume,
            ActivityType = InventoryTransactionType.ProduceProduct,
            UnitPrice = price,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy,
        });

        await db.SaveChangesAsync();
    }

    public async Task<IEnumerable<InventoryTransaction>> SearchInventoryTransactionsAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType)
    {
        using var db = this.contextFactory.CreateDbContext();

        var query = from it in db.InventoryTransactions
                    join inv in db.Inventories on it.InventoryId equals inv.InventoryId
                    where
                        (string.IsNullOrWhiteSpace(inventoryName) || inv.InventoryName.ToLower().IndexOf(inventoryName.ToLower()) >= 0) && (!dateFrom.HasValue || it.TransactionDate >= dateFrom.Value.Date) && (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date) && (!transactionType.HasValue || it.ActivityType == transactionType.Value)
                    select it;

        return await query.Include(x => x.Inventory).ToListAsync();
    }
}

using System;
using AP.CoreBusiness;
using AP.Plugins.EFCoreSQL;
using AP.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AP.Plugins.InMemory;

public class ProductTransactionEFCoreRepositiry : IProductTransactionRepository
{

    private readonly IDbContextFactory<AppDbContext> contextFactory;
    private readonly IProductRepository productRepository;
    private readonly IInventoryTransactionRepository inventoryTransactionRepository;
    private readonly IInventoryRepository inventoryRepository;
    public ProductTransactionEFCoreRepositiry(IDbContextFactory<AppDbContext> contextFactory, IProductRepository productRepository, IInventoryTransactionRepository inventoryTransactionRepository, IInventoryRepository inventoryRepository)
    {
        this.contextFactory = contextFactory;
        this.productRepository = productRepository;
        this.inventoryTransactionRepository = inventoryTransactionRepository;
        this.inventoryRepository = inventoryRepository;
    }

    public async Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy)
    {
        using var db = this.contextFactory.CreateDbContext();

        // Decrease inventories
        var prod = await this.productRepository.FetchProductByIdAsync(product.ProductId);
        if (prod != null)
        {
            foreach (var pi in prod.ProductInventories)
            {
                if (pi.Inventory is not null)
                {
                    // Add inventory transaction
                    var quantToDeduct = pi.InventoryQuantity * quantity;
                    this.inventoryTransactionRepository?.ProduceAsync(productionNumber, pi.Inventory, quantToDeduct, -1, doneBy);

                    // Decrease inventory quantities
                    var inv = await this.inventoryRepository.FetchInventoryByIdAsync(pi.InventoryId);
                    inv.Quantity -= quantToDeduct;
                    await this.inventoryRepository.UpdateInventoryAsync(inv);
                }
            }
        }

        // Add product transaction record
        db.ProductTransactions?.Add(new ProductTransaction
        {
            ProductionNumber = productionNumber,
            ProductId = product.ProductId,
            QuantityBefore = product.Quantity,
            QuantityAfter = product.Quantity + quantity,
            ActivityType = ProductTransactionType.ProduceProduct,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy
        });

        await db.SaveChangesAsync();
    }

    public async Task SellProductAsync(string salesOrderNumber, Product product, int quantity, string doneBy)
    {
        using var db = this.contextFactory.CreateDbContext();

        db.ProductTransactions?.Add(new ProductTransaction
        {
            SONumber = salesOrderNumber,
            ProductId = product.ProductId,
            QuantityBefore = product.Quantity,
            QuantityAfter = product.Quantity - quantity,
            ActivityType = ProductTransactionType.SellProduct,
            UnitPrice = product.Price,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy
        });

        await db.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductTransaction>> SearchProductTransactionsAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
    {
        using var db = this.contextFactory.CreateDbContext();

        var query = from pt in db.ProductTransactions
                    join prod in db.Products on pt.ProductId equals prod.ProductId
                    where
                        (string.IsNullOrWhiteSpace(productName) || prod.ProductName.ToLower().IndexOf(productName.ToLower()) >= 0) && (!dateFrom.HasValue || pt.TransactionDate >= dateFrom.Value.Date) && (!dateTo.HasValue || pt.TransactionDate <= dateTo.Value.Date) && (!transactionType.HasValue || pt.ActivityType == transactionType.Value)
                    select pt;
        return await query.Include(x => x.Product).ToListAsync();
    }
}

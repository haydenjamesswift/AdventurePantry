using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;

namespace AP.Plugins.InMemory;

public class ProductTransactionRepository : IProductTransactionRepository
{

    private List<ProductTransaction> _productTransactions = new List<ProductTransaction>();
    public readonly IProductRepository productRepository;
    public readonly IInventoryTransactionRepository inventoryTransactionRepository;
    public readonly IInventoryRepository inventoryRepository;
    public ProductTransactionRepository(IProductRepository productRepository, IInventoryTransactionRepository inventoryTransactionRepository, IInventoryRepository inventoryRepository)
    {
        this.productRepository = productRepository;
        this.inventoryTransactionRepository = inventoryTransactionRepository;
        this.inventoryRepository = inventoryRepository;
    }

    public async Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy)
    {
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
                    this.inventoryTransactionRepository.ProduceAsync(productionNumber, pi.Inventory, quantToDeduct, -1, doneBy);

                    // Decrease inventory quantities
                    var inv = await this.inventoryRepository.FetchInventoryByIdAsync(pi.InventoryId);
                    inv.Quantity -= quantToDeduct;
                    await this.inventoryRepository.UpdateInventoryAsync(inv);
                }
            }
        }

        // Add product transaction record
        this._productTransactions.Add(new ProductTransaction
        {
            ProductionNumber = productionNumber,
            ProductId = product.ProductId,
            QuantityBefore = product.Quantity,
            QuantityAfter = product.Quantity + quantity,
            ActivityType = ProductTransactionType.ProduceProduct,
            TransactionDate = DateTime.Now,
            DoneBy = doneBy
        });
    }

    public Task SellProductAsync(string salesOrderNumber, Product product, int quantity, string doneBy)
    {
        this._productTransactions.Add(new ProductTransaction
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

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<ProductTransaction>> SearchProductTransactionsAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
    {
        // Get all products by product name
        var products = (await this.productRepository.GetProductsByNameAsync(string.Empty)).ToList();

        var query = from pt in this._productTransactions
                    join prod in products on pt.ProductId equals prod.ProductId
                    where
                        (string.IsNullOrWhiteSpace(productName) || prod.ProductName.ToLower().IndexOf(productName.ToLower()) >= 0) && (!dateFrom.HasValue || pt.TransactionDate >= dateFrom.Value.Date) && (!dateTo.HasValue || pt.TransactionDate <= dateTo.Value.Date) && (!transactionType.HasValue || pt.ActivityType == transactionType.Value)
                    select new ProductTransaction
                    {
                        Product = prod,
                        ProductTransactionId = pt.ProductTransactionId,
                        SONumber = pt.SONumber,
                        ProductionNumber = pt.ProductionNumber,
                        ProductId = pt.ProductId,
                        QuantityBefore = pt.QuantityBefore,
                        QuantityAfter = pt.QuantityAfter,
                        ActivityType = pt.ActivityType,
                        TransactionDate = pt.TransactionDate,
                        DoneBy = pt.DoneBy,
                        UnitPrice = pt.UnitPrice
                    };
        return query;
    }
}

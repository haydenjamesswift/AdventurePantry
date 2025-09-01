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
}

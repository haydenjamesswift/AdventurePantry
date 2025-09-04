using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Products.Interfaces;

namespace AP.UseCases.Products;

public class SellProductsUseCase : ISellProductsUseCase
{
    private readonly IProductTransactionRepository productTransactionRepository;
    private readonly IProductRepository productRepository;
    private readonly IInventoryRepository inventoryRepository;

    public SellProductsUseCase(IProductTransactionRepository productTransactionRepository, IProductRepository productRepository, IInventoryRepository inventoryRepository)
    {
        this.productTransactionRepository = productTransactionRepository;
        this.productRepository = productRepository;
        this.inventoryRepository = inventoryRepository;
    }

    public async Task ExecuteAsync(string salesOrderNumber, Product product, int quantity, string doneBy)
    {
        await productTransactionRepository.SellProductAsync(salesOrderNumber, product, quantity, doneBy);

        product.Quantity -= quantity;

        await this.productRepository.UpdateProductAsync(product);

    }
}

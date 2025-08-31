using System;
using AP.CoreBusiness;
using AP.UseCases.Activities.Interfaces;
using AP.UseCases.PluginInterfaces;

namespace AP.UseCases.Activities;

public class ProduceProductUseCase : IProduceProductUseCase
{

    private readonly IProductTransactionRepository productTransactionRepository;
    private readonly IProductRepository productRepository;

    public ProduceProductUseCase(IProductTransactionRepository productTransactionRepository, IProductRepository productRepository)
    {
        this.productTransactionRepository = productTransactionRepository;
        this.productRepository = productRepository;
    }

    public async Task ExecuteAsync(string productionNumber, Product product, int quantity, string doneBy)
    {
        // Add a transaction record
        await this.productTransactionRepository.ProduceAsync(productionNumber, product, quantity, doneBy);

        // Increase product quantities
        product.Quantity += quantity;
        await this.productRepository.UpdateProductAsync(product);
    }
}

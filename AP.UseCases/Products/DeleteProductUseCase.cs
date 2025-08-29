using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Inventories.Interfaces;
using AP.UseCases.Products.Interfaces;

namespace AP.UseCases.Inventories;

public class DeleteProductUseCase : IDeleteProductUseCase
{
    // Attributes
    private readonly IProductRepository productRepository;

    // Constructor
    public DeleteProductUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    // Function
    public async Task ExecuteAsync(int productId)
    {
        await this.productRepository.DeleteProductAsync(productId);
    }
}

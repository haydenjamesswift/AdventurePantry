using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Inventories;
using AP.UseCases.Products.Interfaces;

public class AddProductUseCase : IAddProductUseCase
{
    private readonly IProductRepository productRepository;

    public AddProductUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task ExecuteAsync(Product product)
    {
        await this.productRepository.AddProductAsync(product);
    }
}

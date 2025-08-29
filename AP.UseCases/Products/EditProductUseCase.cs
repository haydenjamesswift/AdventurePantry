using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Products.Interfaces;

namespace AP.UseCases.Products;

public class EditProductUseCase : IEditProductUseCase
{
    private readonly IProductRepository productRepository;

    public EditProductUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task ExecuteAsync(Product product)
    {
        await this.productRepository.UpdateProductAsync(product);
    }
}

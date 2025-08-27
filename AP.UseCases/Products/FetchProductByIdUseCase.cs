using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Products.Interfaces;

namespace AP.UseCases.Products;

public class FetchProductByIdUseCase : IFetchProductByIdUseCase
{

    private readonly IProductRepository productRepository;

    public FetchProductByIdUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Product> ExecuteAsync(int productId)
    {
        return await productRepository.FetchProductByIdAsync(productId);
    }
}

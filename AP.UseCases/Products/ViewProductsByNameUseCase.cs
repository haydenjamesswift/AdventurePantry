using AP.CoreBusiness;
using AP.UseCases.Products.Interfaces;
using AP.UseCases.PluginInterfaces;

namespace AP.UseCases;

public class ViewProductsByNameUseCase : IViewProductsByNameUseCase
{
    private readonly IProductRepository productRepository;

    public ViewProductsByNameUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> ExecuteAsync(string name = "")
    {
        return await productRepository.GetProductsByNameAsync(name);
    }
}

using System;
using AP.CoreBusiness;

namespace AP.UseCases.PluginInterfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task<Product> FetchProductByIdAsync(int productId);
}

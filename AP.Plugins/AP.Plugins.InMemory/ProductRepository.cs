using System;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;

namespace AP.Plugins.InMemory;

public class ProductRepository : IProductRepository
{
    // Attributes
    private List<Product> _products;

    // Constructor
    public ProductRepository()
    {
        _products = new List<Product>()
        {
            new Product { ProductId = 1, ProductName = "Bike Seat", Quantity = 10, Price = 15},
            new Product { ProductId = 2, ProductName = "Bike Frame", Quantity = 6, Price = 25},
            new Product { ProductId = 3, ProductName = "Bike Pedals", Quantity = 16, Price = 10},
            new Product { ProductId = 4, ProductName = "Bike Wheels", Quantity = 24, Price = 10}
        };
    }

    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {

        if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_products);

        return _products.Where(x => x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase));
    }

    public Task AddProductAsync(Product product)
    {
        // Validate input
        if (product.ProductName is null || product.Quantity < 0 || product.Price < 0)
        {
            return Task.CompletedTask;
        }

        // Generate new ProductId
        var maxId = _products.Max(x => x.ProductId);
        product.ProductId = maxId + 1;

        // Add product
        _products.Add(product);

        return Task.CompletedTask;
    }

    public Task UpdateProductAsync(Product product)
    {

        if (_products.Any(x => x.ProductId != product.ProductId && x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
        {
            return Task.CompletedTask;
        }

        var prodToUpdate = _products.FirstOrDefault(x => x.ProductId == product.ProductId);

        if (prodToUpdate is not null)
        {
            prodToUpdate.ProductName = product.ProductName;
            prodToUpdate.Quantity = product.Quantity;
            prodToUpdate.Price = product.Price;
        }

        return Task.CompletedTask;
    }

    public async Task<Product> FetchProductByIdAsync(int productId)
    {
        return await Task.FromResult(_products.First(x => x.ProductId == productId));
    }

}
 
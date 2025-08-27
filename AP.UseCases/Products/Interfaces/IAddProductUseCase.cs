using AP.CoreBusiness;

namespace AP.UseCases.Products;

public interface IAddProductUseCase
{
    Task ExecuteAsync(Product product);
}

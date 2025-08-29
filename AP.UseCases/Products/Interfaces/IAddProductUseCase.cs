using System;
using AP.CoreBusiness;

namespace AP.UseCases.Products.Interfaces;

public interface IAddProductUseCase
{
    Task ExecuteAsync(Product product);
}

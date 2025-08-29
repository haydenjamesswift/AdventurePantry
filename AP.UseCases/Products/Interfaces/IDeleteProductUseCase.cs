using System;

namespace AP.UseCases.Products.Interfaces;

public interface IDeleteProductUseCase
{
    Task ExecuteAsync(int productId);
}

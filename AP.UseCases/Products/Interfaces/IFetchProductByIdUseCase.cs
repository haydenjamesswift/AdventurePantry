using System;
using AP.CoreBusiness;

namespace AP.UseCases.Products.Interfaces;

public interface IFetchProductByIdUseCase
{
    Task<Product> ExecuteAsync(int productId);
}

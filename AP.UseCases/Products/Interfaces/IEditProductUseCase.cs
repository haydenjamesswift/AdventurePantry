using System;
using AP.CoreBusiness;

namespace AP.UseCases.Products.Interfaces;
public interface IEditProductUseCase
{
    Task ExecuteAsync(Product product);
}


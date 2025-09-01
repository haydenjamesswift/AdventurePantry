using System;
using AP.CoreBusiness;

namespace AP.UseCases.Products.Interfaces;

public interface ISellProductsUseCase
{
    Task ExecuteAsync(string salesOrderNumber, Product product, int quantity, string  doneBy);
}

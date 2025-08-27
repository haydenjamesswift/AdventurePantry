using System;
using AP.CoreBusiness;

namespace AP.UseCases.Products.Interfaces;

public interface IViewProductsByNameUseCase
{
    Task<IEnumerable<Product>> ExecuteAsync(string name = "");
}

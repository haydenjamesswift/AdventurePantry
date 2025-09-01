using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AP.CoreBusiness;
using AP.Plugins.InMemory;
using AP.UseCases.Products;
using AP.WebApp.ViewModels;

namespace AP.WebApp.ViewModelValidations;

public class Sell_EnsureEnoughInventoryToSell : ValidationAttribute
{

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        var salesViewModel = validationContext.ObjectInstance as SalesViewModel;

        if (salesViewModel is not null)
        {
            // Get current quantity of product
            if (salesViewModel.Product is not null)
            {

                if (salesViewModel.Product.Quantity < salesViewModel.QuantityToSell)
                {
                    return new ValidationResult(
                        $"Not enough {salesViewModel.Product.ProductName} to fulfill this sales order.",
                        new[] { validationContext.MemberName ?? string.Empty });
                }
            }
        }
        return ValidationResult.Success;
    }
}

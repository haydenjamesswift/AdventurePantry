using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AP.CoreBusiness;
using AP.Plugins.InMemory;
using AP.UseCases.Products;
using AP.WebApp.ViewModels;

namespace AP.WebApp.ViewModelValidations;

public class Produce_EnsureEnoughInventoryQuantity : ValidationAttribute
{

    protected override ValidationResult?  IsValid(object? value, ValidationContext validationContext)
    {

        var produceViewModel = validationContext.ObjectInstance as ProductionViewModel;

        // Start validation
        if (produceViewModel is not null)
        {

            // Get ProductInventories
            if (produceViewModel.Product is not null)
            {
                var productInventories = produceViewModel.Product.ProductInventories;

                foreach (var pi in productInventories)
                {
                    if (pi.Inventory is not null && pi.InventoryQuantity * produceViewModel.QuantityToProduce > pi.Inventory.Quantity)
                    {
                        return new ValidationResult(
                            $"Not enough {pi.Inventory.InventoryName} to fulfill this production order.",
                            new[] { validationContext.MemberName ?? string.Empty });
                    }

                }

            }
        }

        return ValidationResult.Success;
    }
}

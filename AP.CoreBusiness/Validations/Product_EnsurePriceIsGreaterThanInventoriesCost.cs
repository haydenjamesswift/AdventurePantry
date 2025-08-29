using System;
using System.ComponentModel.DataAnnotations;

namespace AP.CoreBusiness.Validations;

public class Product_EnsurePriceIsGreaterThanInventoriesCost : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var product = validationContext.ObjectInstance as Product;

        if (product != null)
        {
            if (!ValidatePricing(product))
            {
                return new ValidationResult($"The product price is less than the inventory cost: {GetTotalCost(product)}",
                new List<string> { validationContext.MemberName });
            }
        }

        return ValidationResult.Success;
    }

    private double GetTotalCost(Product product)
    {

        if (product is null || product.ProductInventories is null)
        {
            return 0;
        }

        return product.ProductInventories.Sum(x => x.Inventory?.Price * x.InventoryQuantity ?? 0);

    }

    private Boolean ValidatePricing(Product product) {
        if (product.ProductInventories is null || product.ProductInventories.Count <= 0)
        {
            return true;
        }

        if (GetTotalCost(product) > product.Price)
        {
            return false;
        }

        return true;
    }
}

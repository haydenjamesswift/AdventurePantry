using System;
using System.ComponentModel.DataAnnotations;
using AP.CoreBusiness;
using AP.WebApp.ViewModelValidations;

namespace AP.WebApp.ViewModels;

public class ProductionViewModel
{
    [Required]
    public string ProductionNumber { get; set; } = string.Empty;

    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "You must select an product.")]
    public int ProductId { get; set; }

    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quanitity must be greater than or equal to 1.")]
    [Produce_EnsureEnoughInventoryQuantity]
    public int QuantityToProduce { get; set; }

    public Product? Product { get; set; } = null;


}

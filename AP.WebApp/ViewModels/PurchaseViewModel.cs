using System;
using System.ComponentModel.DataAnnotations;

namespace AP.WebApp.ViewModels;

public class PurchaseViewModel
{
    [Required]
    public string PONumber { get; set; } = string.Empty;

    [Range(minimum:1, maximum:int.MaxValue, ErrorMessage = "You must select an inventory.")]
    public int InventoryId { get; set; }

    [Range(minimum:1, maximum:int.MaxValue, ErrorMessage = "Quanitity must be greater than or equal to 1.")]
    public int QuantityToPurchase { get; set; }

    [Range(minimum:0, maximum:double.MaxValue, ErrorMessage = "Cannot enter negative price value") ]
    public double InventoryPrice { get; set; }

}

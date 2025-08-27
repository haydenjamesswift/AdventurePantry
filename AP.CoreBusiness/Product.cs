using System.ComponentModel.DataAnnotations;

namespace AP.CoreBusiness;

public class Product
{
    // Attributes
    public int ProductId { get; set; }

    [Required(ErrorMessage="This field is required.")]
    [StringLength(150)]
    public string ProductName { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage="Error: Quantity must be greater than or equal to 0.")]
    public int Quantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Error: Price must be greater than or equal to 0.")]
    public double Price { get; set; }
}

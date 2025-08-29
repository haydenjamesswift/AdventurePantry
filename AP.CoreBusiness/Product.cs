using System.ComponentModel.DataAnnotations;
using AP.CoreBusiness.Validations;

namespace AP.CoreBusiness;

public class Product
{
    // Attributes
    public int ProductId { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(150)]
    public string ProductName { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "Error: Quantity must be greater than or equal to 0.")]
    public int Quantity { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Error: Price must be greater than or equal to 0.")]
    public double Price { get; set; }

    [Product_EnsurePriceIsGreaterThanInventoriesCost]
    public List<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

    public void AddInventory(Inventory inventory)
    {
        if (!this.ProductInventories.Any(x => x.Inventory is not null && x.InventoryId == inventory.InventoryId))
        {
            this.ProductInventories.Add(new ProductInventory
            {
                InventoryId = inventory.InventoryId,
                Inventory = inventory,
                InventoryQuantity = 1,
                ProductId = this.ProductId,
                Product = this
            });
        }
    }

    public void RemoveInventory(ProductInventory productInventory)
    {
        this.ProductInventories?.Remove(productInventory);
    }
}
 
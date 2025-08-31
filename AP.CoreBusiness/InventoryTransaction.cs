using System;
using System.ComponentModel.DataAnnotations;

namespace AP.CoreBusiness;

public class InventoryTransaction
{
    [Required]
    public int InventoryTransactionId { get; set; }

    public string PONumber { get; set; } = string.Empty;

    public string ProductionNumber { get; set; } = string.Empty;
    public int InventoryId { get; set; }

    [Required]
    public int QuantityBefore { get; set; }

    [Required]
    public int QuantityAfter { get; set; }

    [Required]
    public InventoryTransactionType ActivityType { get; set; }

    [Required]
    public double UnitPrice { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; }

    [Required]
    public string DoneBy { get; set; }
    public Inventory Inventory { get; set; }

}

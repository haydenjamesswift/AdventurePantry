using System;
using System.ComponentModel.DataAnnotations;

namespace AP.CoreBusiness;

public class ProductTransaction
{
    [Required]
    public int ProductTransactionId { get; set; }

    public string SONumber { get; set; } = string.Empty;

    public string ProductionNumber { get; set; } = string.Empty;
    public int ProductId { get; set; }

    [Required]
    public int QuantityBefore { get; set; }

    [Required]
    public int QuantityAfter { get; set; }

    [Required]
    public ProductTransactionType ActivityType { get; set; }

    [Required]
    public double UnitPrice { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; }

    [Required]
    public string DoneBy { get; set; } = string.Empty;
    
    public Product? Product { get; set; }

}

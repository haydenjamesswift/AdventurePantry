using System;
using Microsoft.EntityFrameworkCore;
using AP.CoreBusiness;

namespace AP.Plugins.EFCoreSQL;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {

    }
    public DbSet<Inventory>? Inventories { get; set; }

    public DbSet<Product>? Products { get; set; }

    public DbSet<ProductInventory> ProductInventories { get; set; }

    public DbSet<InventoryTransaction>? InventoryTransactions { get; set; }

    public DbSet<ProductTransaction>? ProductTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define relationships
        modelBuilder.Entity<ProductInventory>()
            .HasKey(pi => new { pi.ProductId, pi.InventoryId });

        modelBuilder.Entity<ProductInventory>()
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductInventories)
            .HasForeignKey(pi => pi.ProductId);

        modelBuilder.Entity<ProductInventory>()
            .HasOne(pi => pi.Inventory)
            .WithMany(i => i.ProductInventories)
            .HasForeignKey(pi => pi.InventoryId);

        // Seed data
        modelBuilder.Entity<Inventory>().HasData(
            new Inventory { InventoryId = 1, InventoryName = "Bike Seat", Quantity = 100, Price = 15 },
            new Inventory { InventoryId = 2, InventoryName = "Bike Frame", Quantity = 100, Price = 25 },
            new Inventory { InventoryId = 3, InventoryName = "Bike Pedal", Quantity = 200, Price = 10 },
            new Inventory { InventoryId = 4, InventoryName = "Bike Wheel", Quantity = 200, Price = 10 }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, ProductName = "Bike", Quantity = 10, Price = 900 },
            new Product { ProductId = 2, ProductName = "Car", Quantity = 6, Price = 4500 }
        );

        modelBuilder.Entity<ProductInventory>().HasData(
            new ProductInventory { ProductId = 1, InventoryId = 1, InventoryQuantity = 1 }, // Seat
            new ProductInventory { ProductId = 1, InventoryId = 2, InventoryQuantity = 1 }, // Body
            new ProductInventory { ProductId = 1, InventoryId = 3, InventoryQuantity = 2 }, // Pedal
            new ProductInventory { ProductId = 1, InventoryId = 4, InventoryQuantity = 2 } // Wheel
        );
        
    }
}

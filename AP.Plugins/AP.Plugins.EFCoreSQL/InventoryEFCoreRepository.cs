using System;
using System.Diagnostics.Metrics;
using AP.CoreBusiness;
using AP.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AP.Plugins.EFCoreSQL;

public class InventoryEFCoreRepository : IInventoryRepository
{
    private readonly IDbContextFactory<AppDbContext> contextFactory;

    public InventoryEFCoreRepository(IDbContextFactory<AppDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public async Task AddInventoryAsync(Inventory inventory)
    {
        // Get db reference
        using var db = this.contextFactory.CreateDbContext();

        // Add to inventories
        db.Inventories?.Add(inventory);
        await db.SaveChangesAsync();
    }

    public async Task DeleteInventoryAsync(int inventoryId)
    {
        // Create db reference
        using var db = this.contextFactory.CreateDbContext();

        // Find inventory
        var inventory = db.Inventories?.Find(inventoryId);

        if (inventory is null)
        {
            return;
        }

        db.Inventories?.Remove(inventory);
        await db.SaveChangesAsync();
        
    }

    public async Task<Inventory> FetchInventoryByIdAsync(int inventoryId)
    {
        using var db = this.contextFactory.CreateDbContext();

        var inventory = await db.Inventories.FindAsync(inventoryId);

        if (inventory is not null) return inventory;

        return new Inventory();
    }

    public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
    {
        using var db = this.contextFactory.CreateDbContext();

        return await db.Inventories.Where(x => x.InventoryName.ToLower().IndexOf(name.ToLower()) >= 0).ToListAsync();
    }

    public async Task UpdateInventoryAsync(Inventory inventory)
    {
        using var db = this.contextFactory.CreateDbContext();

        var inv = db.Inventories.Find(inventory.InventoryId);

        if (inv is not null)
        {
            inv.InventoryName = inventory.InventoryName;
            inv.Price = inventory.Price;
            inv.Quantity = inventory.Quantity;

            await db.SaveChangesAsync();
        }
    }
}

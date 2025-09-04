using AP.Plugins.InMemory;
using AP.UseCases;
using AP.UseCases.Products;
using AP.UseCases.Products.Interfaces;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Inventories;
using AP.UseCases.Inventories.Interfaces;
using AP.WebApp.Sections;
using AP.UseCases.Activities.Interfaces;
using AP.UseCases.Activities;
using AP.UseCases.Reports.Interfaces;
using AP.UseCases.Reports;
using Microsoft.EntityFrameworkCore;
using AP.Plugins.EFCoreSQL;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryManagement"));
});


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


if (builder.Environment.IsEnvironment("Testing"))
{
    StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
    builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>();
    builder.Services.AddSingleton<IProductRepository, ProductRepository>();
    builder.Services.AddSingleton<IInventoryTransactionRepository, InventoryTransactionRepository>();
    builder.Services.AddSingleton<IProductTransactionRepository, ProductTransactionRepository>();
}
else
{
    builder.Services.AddTransient<IInventoryRepository, InventoryEFCoreRepository>();
    builder.Services.AddTransient<IProductRepository, ProductEFCoreRepository>();
    builder.Services.AddTransient<IInventoryTransactionRepository, InventoryTransactionEFCoreRepository>();
    builder.Services.AddTransient<IProductTransactionRepository, ProductTransactionEFCoreRepositiry>();
}
// Inventory Use Cases
builder.Services.AddTransient<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>();
builder.Services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
builder.Services.AddTransient<IEditInventoryUseCase, EditInventoryUseCase>();
builder.Services.AddTransient<IFetchInventoryByIdUseCase, FetchInventoryByIdUseCase>();
builder.Services.AddTransient<IDeleteInventoryUseCase, DeleteInventoryUseCase>();

// Product Use Cases
builder.Services.AddTransient<IViewProductsByNameUseCase, ViewProductsByNameUseCase>();
builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
builder.Services.AddTransient<IFetchProductByIdUseCase, FetchProductByIdUseCase>();
builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();

// Inventory Transaction Use Cases
builder.Services.AddTransient<IPurchaseInventoryUseCase, PurchaseInventoryUseCase>();
builder.Services.AddTransient<IProduceProductUseCase, ProduceProductUseCase>();
builder.Services.AddTransient<ISellProductsUseCase, SellProductsUseCase>();

builder.Services.AddTransient<ISearchInventoryTransactionsUseCase, SearchInventoryTransactionsUseCase>();
builder.Services.AddTransient<ISearchProductTransactionsUseCase, SearchProductTransactionsUseCase>();

 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

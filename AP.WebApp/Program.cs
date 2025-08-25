using AP.Plugins.InMemory;
using AP.UseCases;
using AP.UseCases.PluginInterfaces;
using AP.UseCases.Products.Interfaces;
using AP.WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


// Add 
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

// Add Use Cases
builder.Services.AddTransient<IViewProductsByNameUseCase, ViewProductsByNameUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

using ApiCarrito_PT.Application.Pricing;
using ApiCarrito_PT.Application.Services;
using ApiCarrito_PT.Application.Validation;
using ApiCarrito_PT.Infrastructure.Persistence;
using ApiCarrito_PT.Infrastructure.Catalog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IProductCatalogProvider, InMemoryProductCatalogProvider>();
builder.Services.AddSingleton<ProductSelectionValidator>();
builder.Services.AddSingleton<PriceCalculator>();
builder.Services.AddSingleton<ICartRepository, InMemoryCartRepository>();
builder.Services.AddSingleton<CartService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

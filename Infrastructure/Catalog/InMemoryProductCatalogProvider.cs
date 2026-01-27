namespace ApiCarrito_PT.Infrastructure.Catalog;

public sealed class InMemoryProductCatalogProvider : IProductCatalogProvider
{
    private static readonly ProductCatalog Product_3546345 = new()
    {
        ProductId = 3546345,
        Name = "Mi Producto",
        Price = 11.90m,
        GroupAttributes = new List<GroupAttributeCatalog>
        {
            new()
            {
                GroupAttributeId = "241887",
                QuantityInformation = new QuantityInformationCatalog
                {
                    GroupAttributeQuantity = 1,
                    VerifyValue = "EQUAL_THAN"
                },
                Attributes = new List<AttributeCatalog>
                {
                    new() { AttributeId = 968636, Name = "Pan Premium", MaxQuantity = 1, PriceImpactAmount = 0m },
                    new() { AttributeId = 968637, Name = "Pan Grande",  MaxQuantity = 1, PriceImpactAmount = 0m },
                }
            },
            new()
            {
                GroupAttributeId = "241888",
                QuantityInformation = new QuantityInformationCatalog
                {
                    GroupAttributeQuantity = 4,
                    VerifyValue = "LOWER_EQUAL_THAN"
                },
                Attributes = new List<AttributeCatalog>
                {
                    new() { AttributeId = 968639, Name = "Carne XT",          MaxQuantity = 4, PriceImpactAmount = 6m },
                    new() { AttributeId = 968640, Name = "Carne Vegetal",     MaxQuantity = 4, PriceImpactAmount = 5m },
                    new() { AttributeId = 968641, Name = "Carne Brava",       MaxQuantity = 4, PriceImpactAmount = 4m },
                    new() { AttributeId = 968642, Name = "Carne Tradicional", MaxQuantity = 4, PriceImpactAmount = 4m },
                    new() { AttributeId = 968643, Name = "Pollo",             MaxQuantity = 4, PriceImpactAmount = 3m },
                    new() { AttributeId = 968644, Name = "Chorizo",           MaxQuantity = 4, PriceImpactAmount = 3m },
                }
            },
            new()
            {
                GroupAttributeId = "241889",
                QuantityInformation = new QuantityInformationCatalog
                {
                    GroupAttributeQuantity = 5,
                    VerifyValue = "LOWER_EQUAL_THAN"
                },
                Attributes = new List<AttributeCatalog>
                {
                    new() { AttributeId = 968646, Name = "Queso americano (2 lascas)", MaxQuantity = 5, PriceImpactAmount = 2m },
                    new() { AttributeId = 968647, Name = "Tocino (2 lascas)",          MaxQuantity = 5, PriceImpactAmount = 2m },
                    new() { AttributeId = 968648, Name = "Tomate",                     MaxQuantity = 5, PriceImpactAmount = 0m },
                    new() { AttributeId = 968649, Name = "Lechuga",                    MaxQuantity = 5, PriceImpactAmount = 0m },
                    new() { AttributeId = 968650, Name = "Papas al hilo",              MaxQuantity = 5, PriceImpactAmount = 1m },
                    new() { AttributeId = 968652, Name = "Pickles",                    MaxQuantity = 5, PriceImpactAmount = 0m },
                    new() { AttributeId = 968653, Name = "Cebolla",                    MaxQuantity = 5, PriceImpactAmount = 0m },
                }
            },
            new()
            {
                GroupAttributeId = "241890",
                QuantityInformation = new QuantityInformationCatalog
                {
                    GroupAttributeQuantity = 5,
                    VerifyValue = "LOWER_EQUAL_THAN"
                },
                Attributes = new List<AttributeCatalog>
                {
                    new() { AttributeId = 968655, Name = "Mayonesa",              MaxQuantity = 5, PriceImpactAmount = 0m },
                    new() { AttributeId = 968656, Name = "Salsa Stacker",         MaxQuantity = 5, PriceImpactAmount = 1m },
                    new() { AttributeId = 968657, Name = "Salsa BBQ",             MaxQuantity = 5, PriceImpactAmount = 1m },
                    new() { AttributeId = 968658, Name = "Ketchup",               MaxQuantity = 5, PriceImpactAmount = 0m },
                    new() { AttributeId = 968659, Name = "Mostaza",               MaxQuantity = 5, PriceImpactAmount = 0m },
                    new() { AttributeId = 968660, Name = "Ají",                   MaxQuantity = 5, PriceImpactAmount = 0m },
                    new() { AttributeId = 968661, Name = "Salsa Stacker Gratis",  MaxQuantity = 1, PriceImpactAmount = 0m },
                    new() { AttributeId = 968662, Name = "Salsa BBQ Gratis",      MaxQuantity = 1, PriceImpactAmount = 0m },
                }
            },
            new()
            {
                GroupAttributeId = "241891",
                QuantityInformation = new QuantityInformationCatalog
                {
                    GroupAttributeQuantity = 1,
                    VerifyValue = "EQUAL_THAN"
                },
                Attributes = new List<AttributeCatalog>
                {
                    new() { AttributeId = 968663, Name = "Papa Personal",        MaxQuantity = 1, PriceImpactAmount = 0m },
                    new() { AttributeId = 968664, Name = "Papa Familiar",        MaxQuantity = 1, PriceImpactAmount = 3m },
                    new() { AttributeId = 968665, Name = "Papa Tumbay Mediana",  MaxQuantity = 1, PriceImpactAmount = 1m },
                    new() { AttributeId = 968666, Name = "Papa Tumbay Familiar", MaxQuantity = 1, PriceImpactAmount = 4m },
                    new() { AttributeId = 968667, Name = "Camote Mediano",       MaxQuantity = 1, PriceImpactAmount = 1m },
                    new() { AttributeId = 968668, Name = "Camote Familiar",      MaxQuantity = 1, PriceImpactAmount = 4m },
                }
            },
            new()
            {
                GroupAttributeId = "241892",
                QuantityInformation = new QuantityInformationCatalog
                {
                    GroupAttributeQuantity = 1,
                    VerifyValue = "EQUAL_THAN"
                },
                Attributes = new List<AttributeCatalog>
                {
                    new() { AttributeId = 968670, Name = "Coca-Cola Sin Azúcar 500 ml", MaxQuantity = 1, PriceImpactAmount = 0m },
                    new() { AttributeId = 968671, Name = "Inca Kola Sin Azúcar 500 ml", MaxQuantity = 1, PriceImpactAmount = 0m },
                    new() { AttributeId = 968674, Name = "Fanta 500 ml",                MaxQuantity = 1, PriceImpactAmount = 0m },
                    new() { AttributeId = 968677, Name = "Agua San Luis Sin Gas 625 ml",MaxQuantity = 1, PriceImpactAmount = 0m },
                }
            }
        }
    };

    public ProductCatalog? GetById(long productId)
        => productId == Product_3546345.ProductId ? Product_3546345 : null;
}

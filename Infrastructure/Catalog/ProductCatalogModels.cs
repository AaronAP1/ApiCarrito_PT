namespace ApiCarrito_PT.Infrastructure.Catalog;

public sealed class ProductCatalog
{
    public long ProductId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public List<GroupAttributeCatalog> GroupAttributes { get; init; } = new();
}

public sealed class GroupAttributeCatalog
{
    public string GroupAttributeId { get; init; } = string.Empty;
    public QuantityInformationCatalog QuantityInformation { get; init; } = new();
    public List<AttributeCatalog> Attributes { get; init; } = new();
}

public sealed class QuantityInformationCatalog
{
    public int GroupAttributeQuantity { get; init; }
    public string VerifyValue { get; init; } = string.Empty; // "EQUAL_THAN" o "LOWER_EQUAL_THAN"
}

public sealed class AttributeCatalog
{
    public long AttributeId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int MaxQuantity { get; init; }
    public decimal PriceImpactAmount { get; init; }
}

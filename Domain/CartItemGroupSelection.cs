namespace ApiCarrito_PT.Domain;

public class CartItemGroupSelection
{
    public string GroupAttributeId { get; init; } = string.Empty;

    public List<CartItemAttributeSelection> Selections { get; init; } = new();
}

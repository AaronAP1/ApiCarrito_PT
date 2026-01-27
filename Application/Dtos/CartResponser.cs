namespace ApiCarrito_PT.Application.Dtos;

public sealed class AddCartItemResponse
{
    public Guid ItemId { get; init; }
    public long ProductId { get; init; }
    public string ProductName { get; init; } = string.Empty;

    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal LineTotal { get; init; }
}

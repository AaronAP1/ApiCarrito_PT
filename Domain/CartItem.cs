namespace ApiCarrito_PT.Domain;

public class CartItem
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public long ProductId { get; init; }
    public string ProductName { get; init; } = string.Empty;

    public decimal BasePrice { get; init; }
    public int Quantity { get; private set; }

    public List<CartItemGroupSelection> SelectedGroups { get; init; } = new();

    public CartItem(long productId, string productName, decimal basePrice, int quantity)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be > 0");

        ProductId = productId;
        ProductName = productName;
        BasePrice = basePrice;
        Quantity = quantity;
    }

    public void SetQuantity(int quantity)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be > 0");
        Quantity = quantity;
    }
}

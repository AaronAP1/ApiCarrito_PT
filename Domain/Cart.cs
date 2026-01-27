namespace ApiCarrito_PT.Domain;

public class Cart
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public List<CartItem> Items { get; } = new();

    public CartItem AddItem(CartItem item)
    {
        Items.Add(item);
        return item;
    }

    public CartItem? FindItem(Guid itemId) =>
        Items.FirstOrDefault(x => x.Id == itemId);

    public bool RemoveItem(Guid itemId)
    {
        var item = FindItem(itemId);
        if (item is null) return false;
        return Items.Remove(item);
    }
}

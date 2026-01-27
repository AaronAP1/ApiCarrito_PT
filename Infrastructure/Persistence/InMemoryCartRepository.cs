using ApiCarrito_PT.Domain;

namespace ApiCarrito_PT.Infrastructure.Persistence;

public sealed class InMemoryCartRepository : ICartRepository
{
    private Cart _cart = new();

    public Cart Get() => _cart;

    public void Save(Cart cart) => _cart = cart;
}

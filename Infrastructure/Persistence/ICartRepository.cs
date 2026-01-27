using ApiCarrito_PT.Domain;

namespace ApiCarrito_PT.Infrastructure.Persistence;

public interface ICartRepository
{
    Cart Get();
    void Save(Cart cart);
}
namespace ApiCarrito_PT.Application.Dtos;

public sealed class UpdateCartItemRequest
{
    public int Quantity { get; set; } = 1;

    // Si el cliente manda grupos, reemplazamos la selección completa.
    public List<GroupSelectionRequest> Groups { get; set; } = new();
}

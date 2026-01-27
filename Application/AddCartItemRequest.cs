namespace ApiCarrito_PT.Application.Dtos;

public class AddCartItemRequest
{
    public long ProductId { get; set; }
    public int Quantity { get; set; } = 1;

    public List<GroupSelectionRequest> Groups { get; set; } = new();
}

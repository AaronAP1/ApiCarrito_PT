namespace ApiCarrito_PT.Application.Dtos;

public class GroupSelectionRequest
{
    public string GroupAttributeId { get; set; } = string.Empty;
    public List<AttributeSelectionRequest> Selections { get; set; } = new();
}

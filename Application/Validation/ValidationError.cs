namespace ApiCarrito_PT.Application.Validation;

public sealed class ValidationError
{
    public string Code { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public string? Path { get; init; } // ej: "groups[0].selections[1]"
}

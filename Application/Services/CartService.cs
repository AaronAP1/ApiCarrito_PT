using ApiCarrito_PT.Application.Dtos;
using ApiCarrito_PT.Application.Pricing;
using ApiCarrito_PT.Application.Validation;
using ApiCarrito_PT.Domain;
using ApiCarrito_PT.Infrastructure.Cart;
using ApiCarrito_PT.Infrastructure.Catalog;

namespace ApiCarrito_PT.Application.Services;

public sealed class CartService
{
    private readonly ICartRepository _cartRepo;
    private readonly IProductCatalogProvider _catalogProvider;
    private readonly ProductSelectionValidator _validator;
    private readonly PriceCalculator _priceCalculator;

    public CartService(
        ICartRepository cartRepo,
        IProductCatalogProvider catalogProvider,
        ProductSelectionValidator validator,
        PriceCalculator priceCalculator)
    {
        _cartRepo = cartRepo;
        _catalogProvider = catalogProvider;
        _validator = validator;
        _priceCalculator = priceCalculator;
    }

    public (CartItem? item, List<ValidationError> errors, int statusCode) AddItem(AddCartItemRequest request)
    {
        var catalog = _catalogProvider.GetById(request.ProductId);
        if (catalog is null)
        {
            return (null,
                new List<ValidationError>
                {
                    new() { Code = "PRODUCT_NOT_FOUND", Message = $"ProductId '{request.ProductId}' not found.", Path = "productId" }
                },
                statusCode: 404);
        }

        var errors = _validator.Validate(request, catalog);
        if (errors.Count > 0)
        {
            // 422: reglas/validación de negocio
            return (null, errors, statusCode: 422);
        }

        var unitPrice = _priceCalculator.CalculateUnitPrice(request, catalog);

        // Map request -> Domain CartItem
        var cartItem = new CartItem(
            productId: catalog.ProductId,
            productName: catalog.Name,
            basePrice: unitPrice,
            quantity: request.Quantity
        )
        {
            SelectedGroups = request.Groups.Select(g => new CartItemGroupSelection
            {
                GroupAttributeId = g.GroupAttributeId,
                Selections = g.Selections.Select(s => new CartItemAttributeSelection
                {
                    AttributeId = s.AttributeId,
                    Quantity = s.Quantity
                }).ToList()
            }).ToList()
        };

        var cart = _cartRepo.Get();
        cart.AddItem(cartItem);
        _cartRepo.Save(cart);

        return (cartItem, new List<ValidationError>(), statusCode: 201);
    }

    public Cart GetCart() => _cartRepo.Get();
}

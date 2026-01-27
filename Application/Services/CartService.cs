using ApiCarrito_PT.Application.Dtos;
using ApiCarrito_PT.Application.Pricing;
using ApiCarrito_PT.Application.Validation;
using ApiCarrito_PT.Domain;
using ApiCarrito_PT.Infrastructure.Persistence;
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

    public (CartItem? item, List<ValidationError> errors, int statusCode) UpdateItem(Guid itemId, UpdateCartItemRequest request)
    {
        var cart = _cartRepo.Get();
        var existing = cart.FindItem(itemId);

        if (existing is null)
        {
            return (null,
                new List<ValidationError> { new() { Code = "ITEM_NOT_FOUND", Message = $"ItemId '{itemId}' not found." } },
                statusCode: 404);
        }

        var catalog = _catalogProvider.GetById(existing.ProductId);
        if (catalog is null)
        {
            return (null,
                new List<ValidationError> { new() { Code = "PRODUCT_NOT_FOUND", Message = $"ProductId '{existing.ProductId}' not found." } },
                statusCode: 404);
        }

        // Convertimos UpdateRequest -> AddCartItemRequest para reutilizar el validador y el price calculator
        var validationRequest = new AddCartItemRequest
        {
            ProductId = existing.ProductId,
            Quantity = request.Quantity,
            Groups = request.Groups
        };

        var errors = _validator.Validate(validationRequest, catalog);
        if (errors.Count > 0)
            return (null, errors, statusCode: 422);

        var unitPrice = _priceCalculator.CalculateUnitPrice(validationRequest, catalog);

        existing.SetQuantity(request.Quantity);
        existing.BasePrice = unitPrice; // ⬅️ OJO: si tu BasePrice es init; cambia a set; (te explico abajo)

        existing.SelectedGroups = request.Groups.Select(g => new CartItemGroupSelection
        {
            GroupAttributeId = g.GroupAttributeId,
            Selections = g.Selections.Select(s => new CartItemAttributeSelection
            {
                AttributeId = s.AttributeId,
                Quantity = s.Quantity
            }).ToList()
        }).ToList();

        _cartRepo.Save(cart);

        return (existing, new List<ValidationError>(), statusCode: 200);
    }

    public (bool deleted, List<ValidationError> errors, int statusCode) DeleteItem(Guid itemId)
    {
        var cart = _cartRepo.Get();

        var ok = cart.RemoveItem(itemId);
        if (!ok)
        {
            return (false,
                new List<ValidationError> { new() { Code = "ITEM_NOT_FOUND", Message = $"ItemId '{itemId}' not found." } },
                statusCode: 404);
        }

        _cartRepo.Save(cart);
        return (true, new List<ValidationError>(), statusCode: 204);
    }
    public Cart GetCart() => _cartRepo.Get();

}

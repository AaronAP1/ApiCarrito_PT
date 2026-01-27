using ApiCarrito_PT.Application.Dtos;
using ApiCarrito_PT.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiCarrito_PT.Controllers;

[ApiController]
[Route("cart")]
public sealed class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService;
    }

    // GET /cart
    [HttpGet]
    public IActionResult GetCart()
    {
        var cart = _cartService.GetCart();

        // Respuesta simple (para la prueba): devuelve items con lo esencial
        var result = new
        {
            cart.Id,
            Items = cart.Items.Select(i => new
            {
                i.Id,
                i.ProductId,
                i.ProductName,
                i.Quantity,
                UnitPrice = i.BasePrice,
                LineTotal = i.BasePrice * i.Quantity,
                Groups = i.SelectedGroups.Select(g => new
                {
                    g.GroupAttributeId,
                    Selections = g.Selections.Select(s => new
                    {
                        s.AttributeId,
                        s.Quantity
                    })
                })
            }),
            Total = cart.Items.Sum(x => x.BasePrice * x.Quantity)
        };

        return Ok(result);
    }

    // POST /cart/items
    [HttpPost("items")]
    public IActionResult AddItem([FromBody] AddCartItemRequest request)
    {
        var (item, errors, statusCode) = _cartService.AddItem(request);

        if (statusCode == 404)
            return NotFound(new { errors });

        if (statusCode == 422)
            return UnprocessableEntity(new { errors });

        // 201
        var response = new AddCartItemResponse
        {
            ItemId = item!.Id,
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            Quantity = item.Quantity,
            UnitPrice = item.BasePrice,
            LineTotal = item.BasePrice * item.Quantity
        };

        return Created($"/cart/items/{response.ItemId}", response);
    }
}

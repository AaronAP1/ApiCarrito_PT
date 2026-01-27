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

        // Respuesta simple, devuelve items con lo esencial
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

    // PUT /cart/items/{itemId}
    [HttpPut("items/{itemId:guid}")]
    public IActionResult UpdateItem([FromRoute] Guid itemId, [FromBody] UpdateCartItemRequest request)
    {
        var (item, errors, statusCode) = _cartService.UpdateItem(itemId, request);

        if (statusCode == 404)
            return NotFound(new { errors });

        if (statusCode == 422)
            return UnprocessableEntity(new { errors });

        var response = new AddCartItemResponse
        {
            ItemId = item!.Id,
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            Quantity = item.Quantity,
            UnitPrice = item.BasePrice,
            LineTotal = item.BasePrice * item.Quantity
        };

        return Ok(response);
    }

    // DELETE /cart/items/{itemId}
    [HttpDelete("items/{itemId:guid}")]
    public IActionResult DeleteItem([FromRoute] Guid itemId)
    {
        var (deleted, errors, statusCode) = _cartService.DeleteItem(itemId);

        if (statusCode == 404)
            return NotFound(new { errors });

        return NoContent(); // 204
    }

}

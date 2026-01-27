using ApiCarrito_PT.Application.Dtos;
using ApiCarrito_PT.Infrastructure.Catalog;

namespace ApiCarrito_PT.Application.Pricing;

public sealed class PriceCalculator
{
    public decimal CalculateUnitPrice(AddCartItemRequest request, ProductCatalog catalog)
    {
        // precio unitario (1 producto) = base + sum(impactos)
        var total = catalog.Price;

        foreach (var group in request.Groups)
        {
            var groupCatalog = catalog.GroupAttributes.First(g => g.GroupAttributeId == group.GroupAttributeId);

            foreach (var sel in group.Selections)
            {
                var attr = groupCatalog.Attributes.First(a => a.AttributeId == sel.AttributeId);
                total += sel.Quantity * attr.PriceImpactAmount;
            }
        }

        return total;
    }
}

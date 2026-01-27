using ApiCarrito_PT.Application.Dtos;
using ApiCarrito_PT.Infrastructure.Catalog;

namespace ApiCarrito_PT.Application.Validation;

public sealed class ProductSelectionValidator
{
    public List<ValidationError> Validate(AddCartItemRequest request, ProductCatalog catalog)
    {
        var errors = new List<ValidationError>();

        if (request.ProductId <= 0)
            errors.Add(new() { Code = "PRODUCT_ID_INVALID", Message = "ProductId must be > 0", Path = "productId" });

        if (request.Quantity <= 0)
            errors.Add(new() { Code = "QUANTITY_INVALID", Message = "Quantity must be > 0", Path = "quantity" });

        // --- Validar que no vengan grupos duplicados
        var duplicatedGroups = request.Groups
            .GroupBy(g => g.GroupAttributeId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        foreach (var gid in duplicatedGroups)
            errors.Add(new() { Code = "GROUP_DUPLICATED", Message = $"GroupAttributeId '{gid}' is duplicated.", Path = "groups" });

        // Índices para paths legibles
        for (int gIndex = 0; gIndex < request.Groups.Count; gIndex++)
        {
            var groupReq = request.Groups[gIndex];

            if (string.IsNullOrWhiteSpace(groupReq.GroupAttributeId))
            {
                errors.Add(new()
                {
                    Code = "GROUP_ID_REQUIRED",
                    Message = "GroupAttributeId is required.",
                    Path = $"groups[{gIndex}].groupAttributeId"
                });
                continue;
            }

            var groupCatalog = catalog.GroupAttributes.FirstOrDefault(x => x.GroupAttributeId == groupReq.GroupAttributeId);
            if (groupCatalog is null)
            {
                errors.Add(new()
                {
                    Code = "GROUP_NOT_FOUND",
                    Message = $"GroupAttributeId '{groupReq.GroupAttributeId}' does not exist for this product.",
                    Path = $"groups[{gIndex}].groupAttributeId"
                });
                continue;
            }

            if (groupReq.Selections is null || groupReq.Selections.Count == 0)
            {
                // si el grupo viene, pero vacío, igual es inválido
                errors.Add(new()
                {
                    Code = "GROUP_EMPTY",
                    Message = $"Group '{groupReq.GroupAttributeId}' must include at least one selection.",
                    Path = $"groups[{gIndex}].selections"
                });
                continue;
            }

            // Validar atributos duplicados dentro del grupo
            var dupAttrs = groupReq.Selections
                .GroupBy(s => s.AttributeId)
                .Where(s => s.Count() > 1)
                .Select(s => s.Key)
                .ToList();

            foreach (var aid in dupAttrs)
                errors.Add(new()
                {
                    Code = "ATTRIBUTE_DUPLICATED",
                    Message = $"AttributeId '{aid}' is duplicated in group '{groupReq.GroupAttributeId}'.",
                    Path = $"groups[{gIndex}].selections"
                });

            // Validar cada selección exista y qty esté bien
            int groupSelectedTotal = 0;

            for (int sIndex = 0; sIndex < groupReq.Selections.Count; sIndex++)
            {
                var sel = groupReq.Selections[sIndex];

                if (sel.AttributeId <= 0)
                {
                    errors.Add(new()
                    {
                        Code = "ATTRIBUTE_ID_INVALID",
                        Message = "AttributeId must be > 0",
                        Path = $"groups[{gIndex}].selections[{sIndex}].attributeId"
                    });
                    continue;
                }

                if (sel.Quantity <= 0)
                {
                    errors.Add(new()
                    {
                        Code = "ATTRIBUTE_QUANTITY_INVALID",
                        Message = "Selection quantity must be > 0",
                        Path = $"groups[{gIndex}].selections[{sIndex}].quantity"
                    });
                    continue;
                }

                var attrCatalog = groupCatalog.Attributes.FirstOrDefault(a => a.AttributeId == sel.AttributeId);
                if (attrCatalog is null)
                {
                    errors.Add(new()
                    {
                        Code = "ATTRIBUTE_NOT_FOUND",
                        Message = $"AttributeId '{sel.AttributeId}' does not exist in group '{groupReq.GroupAttributeId}'.",
                        Path = $"groups[{gIndex}].selections[{sIndex}].attributeId"
                    });
                    continue;
                }

                if (sel.Quantity > attrCatalog.MaxQuantity)
                {
                    errors.Add(new()
                    {
                        Code = "ATTRIBUTE_MAX_EXCEEDED",
                        Message = $"AttributeId '{sel.AttributeId}' exceeds maxQuantity={attrCatalog.MaxQuantity}.",
                        Path = $"groups[{gIndex}].selections[{sIndex}].quantity"
                    });
                    continue;
                }

                groupSelectedTotal += sel.Quantity;
            }

            // Validar regla del grupo (EQUAL_THAN / LOWER_EQUAL_THAN)
            var rule = groupCatalog.QuantityInformation.VerifyValue;
            var limit = groupCatalog.QuantityInformation.GroupAttributeQuantity;

            if (rule == "EQUAL_THAN" && groupSelectedTotal != limit)
            {
                errors.Add(new()
                {
                    Code = "GROUP_RULE_EQUAL_THAN",
                    Message = $"Group '{groupReq.GroupAttributeId}' must have exactly {limit} selections (total quantity). Current={groupSelectedTotal}.",
                    Path = $"groups[{gIndex}].selections"
                });
            }
            else if (rule == "LOWER_EQUAL_THAN" && groupSelectedTotal > limit)
            {
                errors.Add(new()
                {
                    Code = "GROUP_RULE_LOWER_EQUAL_THAN",
                    Message = $"Group '{groupReq.GroupAttributeId}' must have at most {limit} selections (total quantity). Current={groupSelectedTotal}.",
                    Path = $"groups[{gIndex}].selections"
                });
            }
        }

        // --- Validar grupos obligatorios (los EQUAL_THAN típicamente son obligatorios en esta prueba)
        // Si el catálogo tiene grupos con EQUAL_THAN, deben venir en el request.
        var requiredGroups = catalog.GroupAttributes
            .Where(g => g.QuantityInformation.VerifyValue == "EQUAL_THAN")
            .Select(g => g.GroupAttributeId)
            .ToHashSet();

        var sentGroups = request.Groups.Select(g => g.GroupAttributeId).ToHashSet();

        foreach (var required in requiredGroups)
        {
            if (!sentGroups.Contains(required))
            {
                errors.Add(new()
                {
                    Code = "REQUIRED_GROUP_MISSING",
                    Message = $"Required group '{required}' is missing.",
                    Path = "groups"
                });
            }
        }

        return errors;
    }
}

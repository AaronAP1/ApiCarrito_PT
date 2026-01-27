namespace ApiCarrito_PT.Infrastructure.Catalog;

public interface IProductCatalogProvider
{
    ProductCatalog? GetById(long productId);
}

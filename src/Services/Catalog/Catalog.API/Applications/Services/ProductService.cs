using Catalog.API.Domain;
using MongoDB.Entities;

namespace Catalog.API.Applications.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync(CancellationToken ct = default);
    Task<Product> GetProductAsync(string id, CancellationToken ct = default);
    Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName, CancellationToken ct = default);
    Task CreateProductAsync(Product product, CancellationToken ct = default);
    Task<bool> UpdateProductAsync(Product product, CancellationToken ct = default);
    Task<bool> DeleteProductAsync(string id);
}

public class ProductService : IProductService
{
    private readonly Find<Product> _products;
    private readonly Update<Product> _updateProduct;

    public ProductService()
    {
        _products = DB.Find<Product>();
        _updateProduct = DB.Update<Product>();
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken ct = default) => await _products.ExecuteAsync(ct);

    public async Task<Product> GetProductAsync(string id, CancellationToken ct = default) => await _products.OneAsync(id, ct);

    public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName, CancellationToken ct = default) => await _products.ManyAsync(f => f.Eq(p => p.Category, categoryName), ct);

    public async Task CreateProductAsync(Product product, CancellationToken ct = default) => await product.SaveAsync(cancellation: ct);

    public async Task<bool> UpdateProductAsync(Product product, CancellationToken ct = default)
    {
        var updateResult = await _updateProduct
            .Match(m => m.Eq(p => p.ID, product.ID))
            .ModifyWith(product)
            .ExecuteAsync(ct);

        return updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        var deleteResult = await DB.DeleteAsync<Product>(id);

        return deleteResult.DeletedCount > 0;
    }
}
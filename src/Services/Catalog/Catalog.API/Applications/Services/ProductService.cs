using Catalog.API.Domain;
using MongoDB.Entities;

namespace Catalog.API.Applications.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync(CancellationToken ct = default);
    Task<Product> GetProductAsync(string id, CancellationToken ct = default);
    Task<IEnumerable<Product>> GetProductByNameAsync(string name, CancellationToken ct = default);
    Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName, CancellationToken ct = default);
    Task CreateProductAsync(Product product, CancellationToken ct = default);
    Task<bool> UpdateProductAsync(Product product, CancellationToken ct = default);
    Task<bool> DeleteProductAsync(string id, CancellationToken ct = default);
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

    public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken ct) => await _products.ExecuteAsync(ct);
    public async Task<Product> GetProductAsync(string id, CancellationToken ct) => await _products.OneAsync(id, ct);
    public async Task<IEnumerable<Product>> GetProductByNameAsync(string name, CancellationToken ct) => await _products.ManyAsync(f => f.Eq(p => p.Name, name), ct);
    public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName, CancellationToken ct) => await _products.ManyAsync(f => f.Eq(p => p.Category, categoryName), ct);
    public async Task CreateProductAsync(Product product, CancellationToken ct) => await product.SaveAsync(cancellation:ct);
    public async Task<bool> UpdateProductAsync(Product product, CancellationToken ct)
    {
        var updateResult = await _updateProduct
            .Match(m => m.Eq(p => p.ID, product.ID))
            .ModifyWith(product)
            .ExecuteAsync(ct);

        return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
    }
    public async Task<bool> DeleteProductAsync(string id, CancellationToken ct)
    {
        var product = await DB.Find<Product>().OneAsync(id, ct);

        if (product != null)
        {
            var deleteResult = await product.DeleteAsync();

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        return false;
    }
}
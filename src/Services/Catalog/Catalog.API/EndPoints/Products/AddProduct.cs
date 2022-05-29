using Commons.Libraries.Commons;
using Catalog.API.Applications.Services;
using Catalog.API.Domain;
namespace Catalog.API.EndPoints.Products;

/// <summary>
/// Add a Product
/// </summary>
public class AddProduct : Endpoint<Product, EmptyResponse>
{
    private readonly IProductService _productService;

    public AddProduct(IProductService productService)
    {
        _productService = productService;
    }

    public override void Configure()
    {
        Post(ProductRoutes.AddProduct);
        Version(1);
        Permissions(Allow.ProductCreate);
    }

    public override async Task HandleAsync(Product request, CancellationToken ct)
        => await _productService.CreateProductAsync(request, ct);
}
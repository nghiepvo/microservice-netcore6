using Catalog.API.Applications.Services;
using Catalog.API.Commons;
using Catalog.API.Domain;

namespace Catalog.API.EndPoints.Products;

public class GetProducts : Endpoint<EmptyRequest, ListResponse<Product>>
{
    private readonly IProductService _productService;

    public GetProducts(IProductService productService)
    {
        _productService = productService;
    }

    public override void Configure()
    {
        Get(ProductRoutes.GetProducts);
        Version(1);
        Permissions(Allow.ProductRead);
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
        => await SendAsync(new ListResponse<Product> { Data = await _productService.GetProductsAsync(ct) }, cancellation: ct);
}
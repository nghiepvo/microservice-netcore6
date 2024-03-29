using Common.Libraries.API.Applications.Services;
using Common.Libraries.API.Domain;
using Common.Libraries.ViewModels;
using Commons.Libraries.Commons;

namespace Common.Libraries.API.EndPoints.Products;

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
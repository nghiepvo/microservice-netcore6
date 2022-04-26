using Catalog.API.Applications.Services;
using Catalog.API.Commons;
using Catalog.API.Domain;

namespace Catalog.API.EndPoints.Products;

public class GetProductById : Endpoint<IdRequest<string>, Product>
{
    private readonly IProductService _productService;

    public GetProductById(IProductService productService)
    {
        _productService = productService;
    }

    public override void Configure()
    {
        Get(ProductRoutes.GetProductById);
        Version(1);
        Permissions(Allow.ProductRead);
    }

    public override async Task HandleAsync(IdRequest<string> request, CancellationToken ct)
        => await SendAsync(await _productService.GetProductAsync(request.Id, ct), cancellation: ct);
}
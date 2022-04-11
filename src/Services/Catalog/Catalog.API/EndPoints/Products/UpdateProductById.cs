using Catalog.API.Applications.Services;
using Catalog.API.Commons;
using Catalog.API.Domain;

namespace Catalog.API.EndPoints.Products;

public class UpdateProductById : Endpoint<Product, BooleanResponse>
{
    private readonly IProductService _productService;

    public UpdateProductById(IProductService productService)
    {
        _productService = productService;
    }

    public override void Configure()
    {
        Put(ProductRoutes.UpdateProductById);
        Version(1);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Product request, CancellationToken ct)
    {
        await SendAsync(new BooleanResponse { Body = await _productService.UpdateProductAsync(request, ct)}, cancellation: ct);
    }
}
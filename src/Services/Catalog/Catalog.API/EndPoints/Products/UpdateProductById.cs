using Catalog.API.Applications.Services;
using Catalog.API.Domain;
using Common.Libraries.ViewModels;
using Commons.Libraries.Commons;

namespace Catalog.API.EndPoints.Products;

public class UpdateProductById : Endpoint<Product, TypeResponse<bool>>
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
        Permissions(Allow.ProductUpdate);
    }

    public override async Task HandleAsync(Product request, CancellationToken ct)
        => await SendAsync(new TypeResponse<bool> { Body = await _productService.UpdateProductAsync(request, ct)}, cancellation: ct);
}
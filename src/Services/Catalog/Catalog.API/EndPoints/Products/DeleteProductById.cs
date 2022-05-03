using Catalog.API.Applications.Services;
using Catalog.API.Commons;

namespace Catalog.API.EndPoints.Products;

public class DeleteProductById : Endpoint<IdRequest<string>, TypeResponse<bool>>
{
    private readonly IProductService _productService;

    public DeleteProductById(IProductService productService)
    {
        _productService = productService;
    }

    public override void Configure()
    {
        Delete(ProductRoutes.DeleteProductById);
        Version(1);
        Permissions(Allow.ProductDelete);
    }

    public override async Task HandleAsync(IdRequest<string> request, CancellationToken ct)
        => await SendAsync(new TypeResponse<bool> { Body = await _productService.DeleteProductAsync(request.Id) }, cancellation: ct);
}
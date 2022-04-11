using Catalog.API.Applications.Services;
using Catalog.API.Commons;
using Catalog.API.Domain;

namespace Catalog.API.EndPoints.Products;

public class DeleteProductById : Endpoint<IdRequest<string>, BooleanResponse>
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
        AllowAnonymous();
    }

    public override async Task HandleAsync(IdRequest<string> request, CancellationToken ct)
    {
        await SendAsync(new BooleanResponse { Body = await _productService.DeleteProductAsync(request.Id, ct)}, cancellation: ct);
    }
}
using Common.Libraries.API.Applications.Services;
using Common.Libraries.API.Domain;
using Common.Libraries.ViewModels;
using Commons.Libraries.Commons;

namespace Common.Libraries.API.EndPoints.Products;

public class GetProductByCategory : Endpoint<TypeRequest<string>, ListResponse<Product>>
{
    private readonly IProductService _productService;

    public GetProductByCategory(IProductService productService)
    {
        _productService = productService;
    }

    public override void Configure()
    {
        Get(ProductRoutes.GetProductByCategory);
        Version(1);
        Permissions(Allow.ProductRead);
    }

    public override async Task HandleAsync(TypeRequest<string> request, CancellationToken ct)
        => await SendAsync(new ListResponse<Product> { Data = await _productService.GetProductByCategoryAsync(request.Payload, ct) }, cancellation: ct);
}
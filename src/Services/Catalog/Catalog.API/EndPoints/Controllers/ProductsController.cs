using Common.Libraries.API.Applications.Services;
using Common.Libraries.API.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Common.Libraries.API.EndPoints.Controllers;

public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [EnableQuery(PageSize = 5)]
    public async Task<IActionResult> Get(ODataQueryOptions<Product> options)
    {
        return Ok(options.ApplyTo((await _productService.GetProductsAsync()).AsQueryable()));
    }
}
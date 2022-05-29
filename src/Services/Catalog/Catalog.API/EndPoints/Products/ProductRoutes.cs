using Common.Libraries.ViewModels;

namespace Catalog.API.EndPoints.Products;
public static class ProductRoutes
{
    private const string Product = "Product";
    public static readonly string GetProducts = Product;
    public static readonly string AddProduct = Product;
     public static readonly string GetProductByCategory = Product + "/GetProductByCategory/{payload}";
    public static readonly string GetProductById = Product.ById();
    public static readonly string UpdateProductById = Product.ById();
    public static readonly string DeleteProductById = Product.ById();
}
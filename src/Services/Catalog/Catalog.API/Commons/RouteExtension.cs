namespace Catalog.API.Commons;

public static class RouteExtension 
{
    private const string IdParam = "/{id}";
    public static string ById(this string route) => route + IdParam;
}
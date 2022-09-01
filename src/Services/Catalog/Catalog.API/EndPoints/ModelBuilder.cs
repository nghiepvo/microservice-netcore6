using Common.Libraries.API.Domain;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace Common.Libraries.API.EndPoints;

public static class ModelBuilder
{
    public static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();
        builder.EntitySet<Product>("Products");
        return builder.GetEdmModel();
    }
}

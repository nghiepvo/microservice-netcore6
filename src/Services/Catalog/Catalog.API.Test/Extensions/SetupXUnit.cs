using Catalog.API.Applications;
using Catalog.API.EndPoints;
using Catalog.API.EndPoints.Controllers;
using Catalog.API.Infrastructures.MongoDB;
using Catalog.API.Test.Extensions.Odata;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Test.Extensions;

public class SetupXUnit : WebODataTestBase<SetupXUnit.Startup>
{
    public class Startup : TestStartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureControllers(typeof(ProductsController));
            services.AddApplication();
            services.AddMongoDB();
            services.AddControllers().AddOData(options => options.AddRouteComponents("odata", ModelBuilder.GetEdmModel()).EnableQueryFeatures(5));
        }
    }
    public SetupXUnit(WebODataTestFixture<Startup> fixture)
        : base(fixture)
    {
    }
}

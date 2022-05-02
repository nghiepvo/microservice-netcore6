using Xunit;

namespace Catalog.API.Test.Extensions.Odata;

public abstract class WebODataTestBase<TStartup> : IClassFixture<WebODataTestFixture<TStartup>> where TStartup : class
{
    protected WebODataTestBase(WebODataTestFixture<TStartup> factory) => Factory = factory ?? throw new ArgumentNullException(nameof(factory));

    private HttpClient _client;
    public virtual HttpClient Client
    {
        get
        {
            if (_client == null)
            {
                _client = Factory.CreateClient();
                _client.Timeout = TimeSpan.FromSeconds(3600);
            }

            return _client;
        }
    }
    public WebODataTestFixture<TStartup> Factory { get; }
}

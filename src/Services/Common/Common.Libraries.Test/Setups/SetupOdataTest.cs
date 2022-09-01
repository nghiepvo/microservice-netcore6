using Common.Libraries.Test.Odata;

namespace Common.Libraries.Test.Setups;
public class SetupOdataTest<T> : WebODataTestBase<T> where T: TestStartupBase
{
    public SetupOdataTest(WebODataTestFixture<T> fixture)
        : base(fixture)
    {
    }
}
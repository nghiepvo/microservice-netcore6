using Common.LibrariesTest.Odata;

namespace Common.LibrariesTest.Setups;
public class SetupOdataTest<T> : WebODataTestBase<T> where T: TestStartupBase
{
    public SetupOdataTest(WebODataTestFixture<T> fixture)
        : base(fixture)
    {
    }
}
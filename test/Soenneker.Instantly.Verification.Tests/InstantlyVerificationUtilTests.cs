using Soenneker.Instantly.Verification.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Instantly.Verification.Tests;

[Collection("Collection")]
public class InstantlyVerificationUtilTests : FixturedUnitTest
{
    private readonly IInstantlyVerificationUtil _util;

    public InstantlyVerificationUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IInstantlyVerificationUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}

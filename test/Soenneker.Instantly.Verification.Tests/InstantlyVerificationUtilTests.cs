using Soenneker.Instantly.Verification.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Instantly.Verification.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class InstantlyVerificationUtilTests : HostedUnitTest
{
    private readonly IInstantlyVerificationUtil _util;

    public InstantlyVerificationUtilTests(Host host) : base(host)
    {
        _util = Resolve<IInstantlyVerificationUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}

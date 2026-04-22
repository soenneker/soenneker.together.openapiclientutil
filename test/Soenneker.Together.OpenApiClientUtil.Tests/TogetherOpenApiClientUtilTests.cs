using Soenneker.Together.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Together.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class TogetherOpenApiClientUtilTests : HostedUnitTest
{
    private readonly ITogetherOpenApiClientUtil _openapiclientutil;

    public TogetherOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<ITogetherOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}

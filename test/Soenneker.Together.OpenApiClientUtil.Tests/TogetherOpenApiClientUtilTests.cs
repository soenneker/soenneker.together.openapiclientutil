using Soenneker.Together.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Together.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class TogetherOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly ITogetherOpenApiClientUtil _openapiclientutil;

    public TogetherOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<ITogetherOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}

using Soenneker.Together.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Together.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface ITogetherOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<TogetherOpenApiClient> Get(CancellationToken cancellationToken = default);
}

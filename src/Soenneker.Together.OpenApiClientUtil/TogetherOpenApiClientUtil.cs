using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.Together.HttpClients.Abstract;
using Soenneker.Together.OpenApiClientUtil.Abstract;
using Soenneker.Together.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Together.OpenApiClientUtil;

/// <inheritdoc cref="ITogetherOpenApiClientUtil" />
public sealed class TogetherOpenApiClientUtil : ITogetherOpenApiClientUtil
{
    private readonly AsyncSingleton<TogetherOpenApiClient> _client;

    public TogetherOpenApiClientUtil(ITogetherOpenApiHttpClient httpClientUtil)
    {
        _client = new AsyncSingleton<TogetherOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

            return new TogetherOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<TogetherOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}

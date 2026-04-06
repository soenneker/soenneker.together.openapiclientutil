using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Together.HttpClients.Abstract;
using Soenneker.Together.OpenApiClientUtil.Abstract;
using Soenneker.Together.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Together.OpenApiClientUtil;

///<inheritdoc cref="ITogetherOpenApiClientUtil"/>
public sealed class TogetherOpenApiClientUtil : ITogetherOpenApiClientUtil
{
    private readonly AsyncSingleton<TogetherOpenApiClient> _client;

    public TogetherOpenApiClientUtil(ITogetherOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<TogetherOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Together:ApiKey");
            string authHeaderValueTemplate = configuration["Together:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new TogetherOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<TogetherOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}

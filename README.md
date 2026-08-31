[![](https://img.shields.io/nuget/v/soenneker.together.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.together.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.together.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.together.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.together.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.together.openapiclientutil/actions/workflows/codeql.yml)
[![](https://img.shields.io/nuget/dt/soenneker.together.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.together.openapiclientutil/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Together.OpenApiClientUtil
Provides lazily initialized, cached access to Together AI's generated OpenAPI client.

## Installation

```bash
dotnet add package Soenneker.Together.OpenApiClientUtil
```

## Configuration

```json
{
  "Together": {
    "ApiKey": "your-api-key"
  }
}
```

## Registration

```csharp
using Soenneker.Together.OpenApiClientUtil.Registrars;

services.AddTogetherOpenApiClientUtilAsScoped();
```

Scoped registration lets the generated-client wrapper follow the current scope while its authenticated HTTP provider remains singleton. Use `AddTogetherOpenApiClientUtilAsSingleton()` when the wrapper itself should be application-wide.

## Usage

```csharp
using Soenneker.Together.OpenApiClient;
using Soenneker.Together.OpenApiClient.Models;
using Soenneker.Together.OpenApiClientUtil.Abstract;

public sealed class TogetherService
{
    private readonly ITogetherOpenApiClientUtil _clients;

    public TogetherService(ITogetherOpenApiClientUtil clients)
    {
        _clients = clients;
    }

    public async ValueTask<WhoamiResponse?> GetAccount(CancellationToken cancellationToken)
    {
        TogetherOpenApiClient client = await _clients.Get(cancellationToken);
        return await client.Whoami.GetAsync(cancellationToken: cancellationToken);
    }
}
```

Do not dispose the generated client returned by `Get()`. The utility owns its cached wrapper, while the registered HTTP provider owns the underlying `HttpClient`.

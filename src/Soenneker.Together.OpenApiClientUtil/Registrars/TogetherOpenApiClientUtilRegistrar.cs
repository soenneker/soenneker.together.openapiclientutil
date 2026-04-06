using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Together.HttpClients.Registrars;
using Soenneker.Together.OpenApiClientUtil.Abstract;

namespace Soenneker.Together.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class TogetherOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="TogetherOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddTogetherOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddTogetherOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ITogetherOpenApiClientUtil, TogetherOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="TogetherOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddTogetherOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddTogetherOpenApiHttpClientAsSingleton()
                .TryAddScoped<ITogetherOpenApiClientUtil, TogetherOpenApiClientUtil>();

        return services;
    }
}

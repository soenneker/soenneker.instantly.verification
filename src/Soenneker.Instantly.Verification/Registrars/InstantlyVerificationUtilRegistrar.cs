using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Instantly.ClientUtil.Registrars;
using Soenneker.Instantly.Verification.Abstract;

namespace Soenneker.Instantly.Verification.Registrars;

/// <summary>
/// Registers Instantly email-verification operations.
/// </summary>
public static class InstantlyVerificationUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IInstantlyVerificationUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddInstantlyVerificationUtilAsSingleton(this IServiceCollection services)
    {
        services.AddInstantlyOpenApiClientUtilAsSingleton()
                .TryAddSingleton<IInstantlyVerificationUtil, InstantlyVerificationUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IInstantlyVerificationUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddInstantlyVerificationUtilAsScoped(this IServiceCollection services)
    {
        services.AddInstantlyOpenApiClientUtilAsSingleton()
                .TryAddScoped<IInstantlyVerificationUtil, InstantlyVerificationUtil>();

        return services;
    }
}

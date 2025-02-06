using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Instantly.Client.Registrars;
using Soenneker.Instantly.Verification.Abstract;

namespace Soenneker.Instantly.Verification.Registrars;

/// <summary>
/// A .NET typesafe implementation of Instantly.ai's Verification API
/// </summary>
public static class InstantlyVerificationUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IInstantlyVerificationUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddInstantlyVerificationUtilAsSingleton(this IServiceCollection services)
    {
        services.AddInstantlyClientAsSingleton()
                .TryAddSingleton<IInstantlyVerificationUtil, InstantlyVerificationUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IInstantlyVerificationUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddInstantlyVerificationUtilAsScoped(this IServiceCollection services)
    {
        services.AddInstantlyClientAsSingleton()
                .TryAddScoped<IInstantlyVerificationUtil, InstantlyVerificationUtil>();

        return services;
    }
}
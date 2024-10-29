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
    public static void AddInstantlyVerificationUtilAsSingleton(this IServiceCollection services)
    {
        services.AddInstantlyClientAsSingleton();
        services.TryAddSingleton<IInstantlyVerificationUtil, InstantlyVerificationUtil>();
    }

    /// <summary>
    /// Adds <see cref="IInstantlyVerificationUtil"/> as a scoped service. <para/>
    /// </summary>
    public static void AddInstantlyVerificationUtilAsScoped(this IServiceCollection services)
    {
        services.AddInstantlyClientAsSingleton();
        services.TryAddScoped<IInstantlyVerificationUtil, InstantlyVerificationUtil>();
    }
}

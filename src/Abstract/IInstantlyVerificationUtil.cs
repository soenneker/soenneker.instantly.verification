using System.Threading.Tasks;
using System.Threading;
using Soenneker.Instantly.OpenApiClient.Models;

namespace Soenneker.Instantly.Verification.Abstract;

/// <summary>
/// A .NET typesafe implementation of Instantly.ai's Verification API
/// </summary>
public interface IInstantlyVerificationUtil
{
    /// <summary>
    /// If an email takes longer than 10 seconds to verify, the endpoint will return the status as "pending". In that case, you may use the /verify/single/status to check the status of the verification job.
    /// </summary>
    /// <remarks>Alternatively, you can send a webhook_url to receive the results instead of polling the status endpoint.</remarks>
    /// <param name="email"></param>
    /// <param name="webhookUri"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<Def3?> Verify(string email, string webhookUri, CancellationToken cancellationToken = default);

    /// <summary>
    /// This endpoint can be used if the /verify/single endpoint takes longer to verify emails, which can happen for certain tricky emails/domains. The result will be available for one day after verification.
    /// </summary>
    /// <remarks>Alternatively, you can send a webhook_url to the /verify/single endpoint to receive the results instead of polling the /status endpoint.</remarks>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<Def3?> GetResult(string email, CancellationToken cancellationToken = default);
}
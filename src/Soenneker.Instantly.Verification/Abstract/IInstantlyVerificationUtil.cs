using System.Threading.Tasks;
using System.Threading;
using Soenneker.Instantly.OpenApiClient.Models;

namespace Soenneker.Instantly.Verification.Abstract;

/// <summary>
/// Starts Instantly email-verification jobs and retrieves their results.
/// </summary>
public interface IInstantlyVerificationUtil
{
    /// <summary>
    /// Starts verification for an email address and requests delivery of the result to a webhook.
    /// </summary>
    /// <remarks>Alternatively, you can send a webhook_url to receive the results instead of polling the status endpoint.</remarks>
    /// <param name="email">The email address to verify.</param>
    /// <param name="webhookUri">The webhook URL to receive the asynchronous result.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The current verification state, or <see langword="null"/> when the API returns no body.</returns>
    ValueTask<EmailVerification?> Verify(string email, string webhookUri, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the latest available verification result for an email address.
    /// </summary>
    /// <remarks>Alternatively, you can send a webhook_url to the /verify/single endpoint to receive the results instead of polling the /status endpoint.</remarks>
    /// <param name="email">The email address previously submitted for verification.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The verification result, or <see langword="null"/> when it is unavailable.</returns>
    ValueTask<EmailVerification?> GetResult(string email, CancellationToken cancellationToken = default);
}

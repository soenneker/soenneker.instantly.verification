using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Extensions.Configuration;
using Soenneker.Instantly.ClientUtil.Abstract;
using Soenneker.Instantly.Verification.Abstract;
using System.Threading.Tasks;
using System.Threading;
using Soenneker.Extensions.ValueTask;
using Soenneker.Instantly.OpenApiClient;
using Soenneker.Instantly.OpenApiClient.Api.V2.EmailVerification;
using Soenneker.Instantly.OpenApiClient.Models;

namespace Soenneker.Instantly.Verification;

/// <inheritdoc cref="IInstantlyVerificationUtil"/>
public sealed class InstantlyVerificationUtil : IInstantlyVerificationUtil
{
    private readonly IInstantlyOpenApiClientUtil _instantlyOpenApiClientUtil;
    private readonly ILogger<InstantlyVerificationUtil> _logger;
    private readonly bool _log;

    public InstantlyVerificationUtil(IInstantlyOpenApiClientUtil instantlyOpenApiClientUtil, ILogger<InstantlyVerificationUtil> logger, IConfiguration config)
    {
        _instantlyOpenApiClientUtil = instantlyOpenApiClientUtil;
        _logger = logger;
        _log = config.GetValue<bool>("Instantly:LogEnabled");
    }

    public async ValueTask<Def3?> Verify(string email, string webhookUri, CancellationToken cancellationToken = default)
    {
        if (_log)
            _logger.LogDebug("Verifying email ({email}) with Instantly...", email);

        InstantlyOpenApiClient client = await _instantlyOpenApiClientUtil.Get(cancellationToken).NoSync();

        var requestBody = new EmailVerificationPostRequestBody
        {
            Email = email,
            WebhookUrl = webhookUri
        };

        Def3? response = await client.Api.V2.EmailVerification.PostAsync(requestBody, cancellationToken: cancellationToken);
        LogResponseIfEnabled(response, email);

        return response;
    }

    public async ValueTask<Def3?> GetResult(string email, CancellationToken cancellationToken = default)
    {
        if (_log)
            _logger.LogDebug("Getting status of email verification result ({email}) from Instantly...", email);

        InstantlyOpenApiClient client = await _instantlyOpenApiClientUtil.Get(cancellationToken).NoSync();

        Def3? response = await client.Api.V2.EmailVerification[email].GetAsync(cancellationToken: cancellationToken);
        LogResponseIfEnabled(response, email);

        return response;
    }

    private void LogResponseIfEnabled(Def3? response, string email)
    {
        if (!_log)
            return;

        if (response == null)
        {
            _logger.LogWarning("Instantly has not returned a response for email verification ({email})", email);
            return;
        }

        if (response.Status == Def3_status.Success)
        {
            if (response.VerificationStatus == Def3_verification_status.Verified)
                _logger.LogDebug("Instantly has said email is good ({email})", email);
            else
                _logger.LogWarning("Instantly has said email is bad ({email})", email);
        }
        else
        {
            _logger.LogDebug("Instantly has said email is pending verification ({email})", email);
        }
    }
}
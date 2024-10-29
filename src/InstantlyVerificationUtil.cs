using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Extensions.Configuration;
using Soenneker.Instantly.Client.Abstract;
using Soenneker.Instantly.Verification.Abstract;
using System.Threading.Tasks;
using System.Threading;
using Soenneker.Instantly.Verification.Requests;
using Soenneker.Instantly.Verification.Responses;
using Soenneker.Extensions.ValueTask;
using System.Net.Http;
using Soenneker.Extensions.HttpClient;
using Soenneker.Instantly.Verification.Enums;

namespace Soenneker.Instantly.Verification;

/// <inheritdoc cref="IInstantlyVerificationUtil"/>
public class InstantlyVerificationUtil : IInstantlyVerificationUtil
{
    private readonly IInstantlyClient _instantlyClient;
    private readonly ILogger<InstantlyVerificationUtil> _logger;

    private readonly string _apiKey;
    private readonly bool _log;

    public InstantlyVerificationUtil(IInstantlyClient instantlyClient, ILogger<InstantlyVerificationUtil> logger, IConfiguration config)
    {
        _instantlyClient = instantlyClient;
        _logger = logger;

        _apiKey = config.GetValueStrict<string>("Instantly:ApiKey");
        _log = config.GetValue<bool>("Instantly:LogEnabled");
    }

    public async ValueTask<InstantlyVerificationResponse?> Verify(string email, string webhookUri, CancellationToken cancellationToken = default)
    {
        var request = new InstantlyVerificationRequest {Email = email, WebhookUrl = webhookUri};

        if (_log)
            _logger.LogDebug("Verifying email ({email}) with Instantly...", email);

        HttpClient client = await _instantlyClient.Get(cancellationToken).NoSync();

        InstantlyVerificationResponse? response = await client
            .SendWithRetryToType<InstantlyVerificationResponse>(HttpMethod.Post, $"verify/single?api_key={_apiKey}", request, logger: _logger, cancellationToken: cancellationToken).NoSync();

        LogResponseIfEnabled(response, email);

        return response;
    }

    public async ValueTask<InstantlyVerificationResponse?> GetResult(string email, CancellationToken cancellationToken = default)
    {
        if (_log)
            _logger.LogDebug("Getting status of email verification result ({email}) from Instantly...", email);

        HttpClient client = await _instantlyClient.Get(cancellationToken).NoSync();

        InstantlyVerificationResponse? response = await client
            .SendWithRetryToType<InstantlyVerificationResponse>(HttpMethod.Get, $"verify/status?api_key={_apiKey}&email={email}", null, logger: _logger, cancellationToken: cancellationToken)
            .NoSync();

        LogResponseIfEnabled(response, email);

        return response;
    }

    private void LogResponseIfEnabled(InstantlyVerificationResponse? response, string email)
    {
        if (!_log)
            return;

        if (response == null)
        {
            _logger.LogWarning("Instantly has not returned a response for email verification ({email})", email);
            return;
        }

        if (response.Status == InstantlyVerificationJobStatus.Pending)
            _logger.LogDebug("Instantly has said email is pending verification ({email})", email);
        else if (response.Status == InstantlyVerificationJobStatus.Success)
        {
            if (response.VerificationStatus == VerificationStatus.Valid)
                _logger.LogDebug("Instantly has said email is good ({email})", email);
            else
                _logger.LogWarning("Instantly has said email is bad ({email})", email);
        }
    }
}
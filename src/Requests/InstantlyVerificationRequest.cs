using System.Text.Json.Serialization;

namespace Soenneker.Instantly.Verification.Requests;

/// <summary>
/// Represents a request to verify a single email address.
/// If the email verification takes longer than 10 seconds, the endpoint returns the status as “pending.”
/// Use the /verify/single/status endpoint to check the verification job’s status, or provide a webhook_url to receive the results directly.
/// </summary>
public record InstantlyVerificationRequest
{
    /// <summary>
    /// The email address to verify.
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;

    /// <summary>
    /// Optional webhook URL to receive the results instead of polling the status endpoint.
    /// </summary>
    [JsonPropertyName("webhook_url")]
    public string? WebhookUrl { get; set; }
}
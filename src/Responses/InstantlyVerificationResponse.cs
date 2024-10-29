using System.Text.Json.Serialization;
using Ardalis.SmartEnum.SystemTextJson;
using Soenneker.Instantly.Verification.Enums;

namespace Soenneker.Instantly.Verification.Responses;

/// <summary>
/// Represents a response for the email verification result.
/// </summary>
public record InstantlyVerificationResponse
{
    /// <summary>
    /// Verification job status, indicating whether the verification job is complete or pending.
    /// Enum values:
    /// - success: The verification job is finished, and the final verification result is returned.
    /// - pending: The email is pending verification; check the status in a few minutes.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonConverter(typeof(SmartEnumNameConverter<InstantlyVerificationJobStatus, int>))]
    public InstantlyVerificationJobStatus? Status { get; set; }

    /// <summary>
    /// The email address submitted for verification.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// The verification result for the email.
    /// Enum values:
    /// - valid: The email is valid and safe to send emails to.
    /// - invalid: The email is invalid, or risky to send emails to.
    /// </summary>
    [JsonPropertyName("verification_status")]
    [JsonConverter(typeof(SmartEnumNameConverter<VerificationStatus, int>))]
    public VerificationStatus? VerificationStatus { get; set; }

    /// <summary>
    /// Indicates whether this email is in a catch-all domain.
    /// </summary>
    [JsonPropertyName("catch_all")]
    public bool? CatchAll { get; set; }

    /// <summary>
    /// Your remaining verification credit balance.
    /// </summary>
    [JsonPropertyName("credits")]
    public int? Credits { get; set; }

    /// <summary>
    /// The number of credits used for this verification.
    /// </summary>
    [JsonPropertyName("credits_used")]
    public int? CreditsUsed { get; set; }
}
using Ardalis.SmartEnum;

namespace Soenneker.Instantly.Verification.Enums;

/// <summary>
/// Enum for possible verification job statuses of an email.
/// </summary>
public class InstantlyVerificationJobStatus : SmartEnum<InstantlyVerificationJobStatus>
{
    /// <summary>
    /// Indicates the verification job is finished, and the final verification result is returned.
    /// </summary>
    public static readonly InstantlyVerificationJobStatus Success = new("success", 0);

    /// <summary>
    /// Indicates that the email is pending verification and that you should check the status in a few minutes.
    /// </summary>
    public static readonly InstantlyVerificationJobStatus Pending = new("pending", 1);

    private InstantlyVerificationJobStatus(string name, int value) : base(name, value)
    {
    }
}
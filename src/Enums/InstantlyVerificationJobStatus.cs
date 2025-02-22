using Intellenum;

namespace Soenneker.Instantly.Verification.Enums;

/// <summary>
/// Enum for possible verification job statuses of an email.
/// </summary>
[Intellenum<string>]
public partial class InstantlyVerificationJobStatus
{
    /// <summary>
    /// Indicates the verification job is finished, and the final verification result is returned.
    /// </summary>
    public static readonly InstantlyVerificationJobStatus Success = new("success");

    /// <summary>
    /// Indicates that the email is pending verification and that you should check the status in a few minutes.
    /// </summary>
    public static readonly InstantlyVerificationJobStatus Pending = new("pending");
}
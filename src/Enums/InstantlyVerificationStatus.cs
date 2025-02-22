using Intellenum;

namespace Soenneker.Instantly.Verification.Enums;

/// <summary>
/// Enum for possible verification status results of an email.
/// </summary>
[Intellenum<string>]
public partial class VerificationStatus
{
    /// <summary>
    /// The email is valid and safe to send emails to.
    /// </summary>
    public static readonly VerificationStatus Valid = new("valid");

    /// <summary>
    /// The email is invalid, or is risky to send emails to.
    /// </summary>
    public static readonly VerificationStatus Invalid = new("invalid");
}
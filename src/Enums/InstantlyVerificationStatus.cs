using Ardalis.SmartEnum;

namespace Soenneker.Instantly.Verification.Enums;

/// <summary>
/// Enum for possible verification status results of an email.
/// </summary>
public class VerificationStatus : SmartEnum<VerificationStatus>
{
    /// <summary>
    /// The email is valid and safe to send emails to.
    /// </summary>
    public static readonly VerificationStatus Valid = new("valid", 0);

    /// <summary>
    /// The email is invalid, or is risky to send emails to.
    /// </summary>
    public static readonly VerificationStatus Invalid = new("invalid", 1);

    private VerificationStatus(string name, int value) : base(name, value)
    {
    }
}
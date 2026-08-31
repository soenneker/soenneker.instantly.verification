[![](https://img.shields.io/nuget/v/soenneker.instantly.verification.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.instantly.verification/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.instantly.verification/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.instantly.verification/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.instantly.verification.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.instantly.verification/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.instantly.verification/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.instantly.verification/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Instantly.Verification

Start Instantly email-verification jobs with webhook delivery and retrieve results by email address.

## Install

```bash
dotnet add package Soenneker.Instantly.Verification
```

## Configure and register

```json
{
  "Instantly": {
    "ApiKey": "<API key>",
    "LogEnabled": false
  }
}
```

```csharp
using Soenneker.Instantly.Verification.Registrars;

services.AddInstantlyVerificationUtilAsScoped();
```

The scoped verification service deliberately uses the singleton generated-client provider. Use `AddInstantlyVerificationUtilAsSingleton()` when the operation layer should also live for the application lifetime.

## Start verification

```csharp
using Soenneker.Instantly.OpenApiClient.Models;
using Soenneker.Instantly.Verification.Abstract;

EmailVerification? verification = await verifier.Verify(
    "person@example.com",
    "https://example.com/webhooks/instantly-verification",
    cancellationToken);
```

The webhook URL must be reachable by Instantly. Verification may initially be `Pending`; webhook delivery avoids polling when processing takes longer.

## Retrieve a result

```csharp
EmailVerification? result = await verifier.GetResult(
    "person@example.com",
    cancellationToken);
```

Instantly retains a verification result for a limited period. Inspect `result.Status` for request success and `result.VerificationStatus` for `Pending`, `Verified`, or `Invalid`.

When `Instantly:LogEnabled` is true, the service logs email addresses and verification outcomes. Leave it disabled when those addresses should not appear in application logs.

API failures are surfaced to the caller. Nullable results indicate that Instantly returned no response body or no available result.


# PasswordResetResponse Class Documentation

## Overview

The `PasswordResetResponse` class represents the response for a password reset request in the Flameborn SDK. This class extends `ResponseEntity` and implements the `IPasswordResetResponse` interface.

### Namespace
```csharp
namespace flameborn.Sdk.Requests.Login.Entity
```

## Inheritance
- `ResponseEntity`
- Implements: `IPasswordResetResponse`

## Attributes
- `[Serializable]`

## Description
This class serves as a response entity for password reset requests.

### Example
```csharp
PasswordResetResponse passwordResetResponse = new PasswordResetResponse();
// Use passwordResetResponse in your logic
```

## References
- [IPasswordResetResponse](https://github.com/gkhanC/flameborn-game/tree/dev/documents/IPasswordResetResponse)
- [ResponseEntity](https://github.com/gkhanC/flameborn-game/tree/dev/documents/ResponseEntity)

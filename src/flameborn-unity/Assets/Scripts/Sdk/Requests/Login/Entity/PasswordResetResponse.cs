using System;
using flameborn.Sdk.Requests.Entity;
using flameborn.Sdk.Requests.Login.Abstract;

namespace flameborn.Sdk.Requests.Login.Entity
{
    /// <summary>
    /// Represents the response for a password reset request.
    /// </summary>
    [Serializable]
    public class PasswordResetResponse : ResponseEntity, IPasswordResetResponse
    {
        // This class serves as a response entity for password reset requests.
    }
}

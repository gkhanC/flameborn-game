using System;
using flameborn.Sdk.Requests.Entity;
using flameborn.Sdk.Requests.Login.Abstract;

namespace flameborn.Sdk.Requests.Login.Entity
{
    /// <summary>
    /// Represents the response for a register request.
    /// </summary>
    [Serializable]
    public class RegisterResponse : ResponseEntity, IRegisterResponse
    {
        // This class serves as a response entity for register requests.
    }
}

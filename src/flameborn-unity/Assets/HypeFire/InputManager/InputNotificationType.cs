using System;

namespace HF.InputManager
{
    /// <summary>
    /// This base class represents a type of input notification. Derived classes
    /// will handle specific input events and provide relevant data.
    /// </summary>
    [Serializable]
    public abstract class InputEvent
    {
        protected InputEvent() { }

        /// <summary>
        /// Attempts to read data associated with the specific input notification type.
        /// </summary>
        /// <param name="inputData">The output parameter that will receive the data object if successful, null otherwise.</param>
        /// <returns>True if the input notification data was successfully read, false otherwise.</returns>
        public abstract bool ReadInput(out object inputData);
    }
}

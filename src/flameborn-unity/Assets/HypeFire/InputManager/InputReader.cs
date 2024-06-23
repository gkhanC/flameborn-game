using System;

namespace HF.InputManager
{
    /// <summary>
    /// This class manages reading input based on a specified InputEvent.
    /// </summary>
    [Serializable]
    public class InputReader
    {        
        private readonly InputEvent m_InputEvent;

        /// <summary>
        /// Attempts to read the input based on the configured InputEvent.
        /// </summary>
        /// <param name="inputData">The output parameter that will receive the data object if successful, null otherwise.</param>
        /// <returns>True if the input was successfully read, false otherwise.</returns>
        public bool ReadInput(out object inputData) => m_InputEvent.ReadInput(out inputData);

        /// <summary>
        /// Constructor for InputReader class.
        /// </summary>
        /// <param name="event">The type of input notification to use for reading input.</param>
        public InputReader(InputEvent @event)
        {
            m_InputEvent = @event;
        }
    }
}

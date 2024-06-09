using System;
using UnityEngine;

namespace HF.InputManager
{
    /// <summary>
    /// This class represents an input notification for a button up event.
    /// </summary>
    public class OnKeyUp : InputEvent
    {
        [SerializeField]
        [Tooltip("The name of the button to monitor for a up event.")]
        private OnKeyUpData data;

        /// <summary>
        /// Gets the name of the button associated with this notification.
        /// </summary>
        public KeyCode ButtonName => data.keyCode;

        /// <summary>
        /// Attempts to read the button up notification data.
        /// </summary>
        /// <param name="inputData">The output parameter that will receive the data object if successful, null otherwise.</param>
        /// <returns>True if a button up event was detected for the configured button, false otherwise.</returns>
        public override bool ReadInput(out object inputData)
        {
            return TryGetButtonData(out inputData);
        }

        private bool TryGetButtonData(out object inputData)
        {
            if (Input.GetKeyUp(data.keyCode))
            {
                data.pressCount++;
                data.lastPressTime = DateTime.Now;
                inputData = data;
                return true;
            }

            inputData = null;
            return false;
        }

        /// <summary>
        /// Constructor for OnButtonDown class.
        /// </summary>
        /// <param name="keyCode">The name of the button to monitor for a up event.</param>
        public OnKeyUp(KeyCode keyCode)
        {
            data = new OnKeyUpData();
            data.keyCode = keyCode;
        }

        [Serializable]
        public struct OnKeyUpData
        {
            /// <summary>
            /// The name of the button associated with this notification.
            /// </summary>
            public KeyCode keyCode;

            public int pressCount; // Consider tracking press count if needed
            public DateTime lastPressTime; // Consider tracking last press time if needed
        }
    }
}

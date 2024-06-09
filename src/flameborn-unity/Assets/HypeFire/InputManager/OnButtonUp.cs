using System;
using UnityEngine;

namespace HF.InputManager
{
    /// <summary>
    /// This class represents an input notification for a button up continues event.
    /// </summary>
    public class OnButtonUp : InputEvent
    {
        [SerializeField]
        [Tooltip("The name of the button up to monitor for continues event.")]
        private OnButtonUpData data;

        /// <summary>
        /// Gets the name of the button up associated with this notification.
        /// </summary>
        public string ButtonName => data.buttonName;

        /// <summary>
        /// Attempts to read the button up notification data.
        /// </summary>
        /// <param name="inputData">The output parameter that will receive the data object if successful, null otherwise.</param>
        /// <returns>True if a button event was detected for the configured button, false otherwise.</returns>
        public override bool ReadInput(out object inputData)
        {
            return TryGetButtonData(out inputData);
        }

        private bool TryGetButtonData(out object inputData)
        {

            if (Input.GetButtonUp(data.buttonName))
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
        /// Constructor for OnButtonUp class.
        /// </summary>
        /// <param name="buttonName">The name of the button up to monitor for a event.</param>
        public OnButtonUp(string buttonName)
        {
            data = new OnButtonUpData();
            data.buttonName = buttonName;
        }

        [Serializable]
        public struct OnButtonUpData
        {
            /// <summary>
            /// The name of the button up associated with this notification.
            /// </summary>
            public string buttonName;

            public int pressCount; // Consider tracking press count if needed
            public DateTime lastPressTime; // Consider tracking last press time if needed
        }
    }
}

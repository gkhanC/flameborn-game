using System;
using UnityEngine;

namespace HF.InputManager
{
    /// <summary>
    /// This class represents an input notification for a button continues event.
    /// </summary>
    public class OnButton : InputEvent
    {
        [SerializeField]
        [Tooltip("The name of the button to monitor for continues event.")]
        private OnButtonData data;

        /// <summary>
        /// Gets the name of the button associated with this notification.
        /// </summary>
        public string ButtonName => data.buttonName;

        /// <summary>
        /// Attempts to read the button continues notification data.
        /// </summary>
        /// <param name="inputData">The output parameter that will receive the data object if successful, null otherwise.</param>
        /// <returns>True if a button event was detected for the configured button, false otherwise.</returns>
        public override bool ReadInput(out object inputData)
        {
            return TryGetButtonData(out inputData);
        }

        private bool TryGetButtonData(out object inputData)
        {
            if (Input.GetButtonDown(data.buttonName))
            {
                data.pressCount++;
            }

            if (Input.GetButton(data.buttonName))
            {
                data.lastPressTime = DateTime.Now;
                inputData = data;
                return true;
            }

            inputData = null;
            return false;
        }

        /// <summary>
        /// Constructor for OnButton class.
        /// </summary>
        /// <param name="buttonName">The name of the button to monitor for a event.</param>
        public OnButton(string buttonName)
        {
            data = new OnButtonData();
            data.buttonName = buttonName;
        }

        [Serializable]
        public struct OnButtonData
        {
            /// <summary>
            /// The name of the button associated with this notification.
            /// </summary>
            public string buttonName;

            public int pressCount; // Consider tracking press count if needed
            public DateTime lastPressTime; // Consider tracking last press time if needed
        }
    }
}

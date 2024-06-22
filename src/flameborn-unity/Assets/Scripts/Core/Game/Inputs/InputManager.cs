using System;
using flameborn.Core.Game.Inputs.Abstract;
using flameborn.Core.Game.Inputs.Controllers;
using UnityEngine;
using UnityEngine.Events;

namespace flameborn.Core.Game.Inputs
{
    /// <summary>
    /// Manages input and handles input events.
    /// </summary>
    public class InputManager : MonoBehaviour, IInputManager<ScreenTouchController, TouchResult>
    {
        // Public properties
        /// <summary>
        /// Global access to the InputManager.
        /// </summary>
        public static InputManager GlobalAccess { get; private set; }

        /// <summary>
        /// The ScreenTouchController instance.
        /// </summary>
        public ScreenTouchController Controller { get; private set; }

        /// <summary>
        /// The layer mask for selectable objects.
        /// </summary>
        public LayerMask selectableObjectsLayers;

        // Private fields
        private UnityEvent<TouchResult> event_OnInput;

        /// <summary>
        /// Subscribes a listener to the input event.
        /// </summary>
        /// <param name="listener">The listener to subscribe.</param>
        public void SubscribeInputController(Action<TouchResult> listener)
        {
            event_OnInput.AddListener(new UnityAction<TouchResult>(listener));
        }

        private void Awake()
        {
            GlobalAccess = this;
            Controller = new ScreenTouchController(selectableObjectsLayers);
            event_OnInput = new UnityEvent<TouchResult>();
        }

        private void Update()
        {
            Controller.HandleInput(event_OnInput.Invoke);
        }
    }
}

using System;
using flameborn.Core.Game.Inputs.Abstract;
using flameborn.Core.Game.Inputs.Controllers;
using UnityEngine;
using UnityEngine.Events;
namespace flameborn.Core.Game.Inputs
{
    public class InputManager : MonoBehaviour, IInputManager<ScreenTouchController, TouchResult>
    {
        public LayerMask selectableObjectsLayers;
        public static InputManager GlobalAccess { get; private set; }
        public ScreenTouchController Controller { get; private set; }

        private UnityEvent<TouchResult> event_OnInput;

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
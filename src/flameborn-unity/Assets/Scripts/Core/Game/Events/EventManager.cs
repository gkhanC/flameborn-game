using flameborn.Core.Game.Inputs;
using flameborn.Core.Game.Inputs.Abstract;
using flameborn.Core.Game.Inputs.Controllers;
using flameborn.Core.Game.Objects.Abstract;
using flameborn.Core.Game.Player;
using HF.Extensions;
using Photon.Pun;
using UnityEngine;

namespace flameborn.Core.Game.Events
{
    /// <summary>
    /// Manages game events and input handling.
    /// </summary>
    public class EventManager : MonoBehaviour, IInputListener<TouchResult>
    {
        // Public variables
        public static EventManager GlobalAccess { get; private set; }

        /// <summary>
        /// The local player's campfire.
        /// </summary>
        public PlayerCampFire localPlayer;

        // Private variables
        private TouchResult touchResult;
        public ISelectable current;
        private GameObject newSelectedObject;
        private GameObject oldSelectedObject;

        private void Awake()
        {
            GlobalAccess = this;
        }

        private void Start()
        {
            InputManager.GlobalAccess.SubscribeInputController(InputListener);
        }

        private void Update()
        {
            // Update logic can be implemented here
        }

        /// <summary>
        /// Sets the local player reference.
        /// </summary>
        /// <param name="campFire">The local player's campfire.</param>
        public void SetLocalPlayer(PlayerCampFire campFire)
        {
            localPlayer = campFire;
        }

        /// <summary>
        /// Handles input events.
        /// </summary>
        /// <param name="result">The touch result from input.</param>
        public void InputListener(TouchResult result)
        {
            touchResult = result;
            CheckInput();
        }

        private void CheckInput()
        {
            if (localPlayer.IsNull() || touchResult == null) return;

            switch (touchResult.status)
            {
                case InputStatus.ObjectSelect:
                    HandleObjectSelect();
                    break;
                case InputStatus.TouchCompleted:
                    HandleTouchCompleted();
                    break;
            }

            if (touchResult.status == InputStatus.ObjectSelect)
            {
                touchResult.status = InputStatus.None;
            }
        }

        private void HandleObjectSelect()
        {

            if (current == null)
            {
                SelectNewObject(touchResult.selectedObject);

            }
            else
            {
                var view = touchResult.selectedObject.GetComponent<PhotonView>();
                var selectable = touchResult.selectedObject.GetComponent<ISelectable>();
                if (view != null && view.IsMine)
                {
                    if (selectable != current)
                    {
                        current.DeSelect();
                    }

                    SelectNewObject(touchResult.selectedObject);
                }
                else
                {
                    if (current.selectableType == SelectableTypes.Worker)
                    {
                        current.SetTarget(selectable);
                        current = null;
                    }
                }
            }
        }

        private void HandleTouchCompleted()
        {
            if (current == null) return;

            if (current.selectableType == SelectableTypes.Worker)
            {
                current.SetDestination(touchResult.startPosition);
            }

        }

        private void SelectNewObject(GameObject selectedObject)
        {
            var view = selectedObject.GetComponent<PhotonView>();
            if (view == null || !view.IsMine) return;

            var selectable = selectedObject.GetComponent<ISelectable>();
            if (selectable == null) return;

            selectable.Select();
            current = selectable;
        }
    }
}

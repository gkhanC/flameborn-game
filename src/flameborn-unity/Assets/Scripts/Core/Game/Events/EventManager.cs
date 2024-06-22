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
    public class EventManager : MonoBehaviour, IInputListener<TouchResult>
    {
        TouchResult touchResult;
        public PlayerCampFire localPlayer;
        public static EventManager GlobalAccess { get; private set; }

        GameObject neww; GameObject old;
        public ISelectable current;

        public void InputListener(TouchResult result)
        {
            touchResult = result;
            CheckInput();
        }

        public void SetLocalPlayer(PlayerCampFire campFire)
        {
            localPlayer = campFire;
        }

        public void CheckInput()
        {
            if (localPlayer.IsNull()) return;
            if (touchResult == null) return;

            if (touchResult.status == InputStatus.ObjectSelect)
            {
                if (current == null)
                {
                    var view = touchResult.selectedObject.GetComponent<PhotonView>();
                    if (view.IsMine)
                    {
                        var selectable = touchResult.selectedObject.GetComponent<ISelectable>();
                        selectable.Select();
                        current = selectable;
                        neww = touchResult.selectedObject;
                        Debug.LogError("A");
                    }
                }
                else
                {
                    var view = touchResult.selectedObject.GetComponent<PhotonView>();
                    var selectable = touchResult.selectedObject.GetComponent<ISelectable>();
                    if (view != null && view.IsMine)
                    {
                        if (selectable != current) current.DeSelect();

                        selectable.Select();
                        current = selectable;
                        old = neww;
                        neww = touchResult.selectedObject;
                        Debug.LogError("B");
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
            else if (touchResult.status == InputStatus.TouchCompleted)
            {
                if (current != null)
                {
                    if (current.selectableType == SelectableTypes.Worker)
                    {
                        current.SetDestination(touchResult.startPosition);
                        current = null;
                        neww = null;
                        Debug.LogError("D");
                    }
                    else
                    {
                        current.DeSelect();
                        Debug.LogError("E");
                    }

                }
            }

            if (touchResult.status == InputStatus.ObjectSelect)
            {
                touchResult.status = InputStatus.None;
            }

        }


        private void Awake()
        {
            GlobalAccess = this;
        }

        private void Start()
        {
            InputManager.GlobalAccess.SubscribeInputController(this.InputListener);
        }

        private void Update()
        {

        }
    }
}
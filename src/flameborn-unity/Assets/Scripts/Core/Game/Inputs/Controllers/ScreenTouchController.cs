using System;
using flameborn.Core.Game.Inputs.Controllers.Abstract;
using flameborn.Core.Game.Objects.Abstract;
using HF.Extensions;
using UnityEngine;

namespace flameborn.Core.Game.Inputs.Controllers
{
    [Serializable]
    public class ScreenTouchController : IInputController
    {
        [field: SerializeField]
        private float stepLength = 1f;
        [field: SerializeField]
        private float inputContinuosDelay = .15f;
        private float inputContinuosTimer = 0f;

        [field: SerializeField]
        private LayerMask RayMask { get; set; } = 0;

        public TouchResult result;

        public ScreenTouchController(LayerMask rayMask)
        {
            RayMask = rayMask;
        }

        private bool FindSelectableObjects()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, RayMask))
            {
                result.startPosition = hitInfo.point;
                if (hitInfo.collider.gameObject.tag != "Ground")
                {
                    result.status = InputStatus.ObjectSelect;
                    result.selectedObject = hitInfo.collider.gameObject;
                }

                return true;
            }

            return false;
        }

        public void HandleInput(params Action<TouchResult>[] inputListeners)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                result = new TouchResult();
                result.status = InputStatus.Touched;
                inputContinuosTimer = 0f;

                if (!FindSelectableObjects())
                {
                    result.startPosition = Input.mousePosition;
                }
            }

            if (Input.GetButton("Fire1"))
            {
                inputContinuosTimer += Time.deltaTime;
                if (inputContinuosTimer >= inputContinuosDelay)
                {
                    if (result.status != InputStatus.Continuos && Input.mousePosition == result.startPosition) return;

                    if (Input.mousePosition != result.endPosition)
                    {
                        if (result.endPosition != Vector3.zero && Vector3.Distance(result.endPosition, Input.mousePosition) >= 2f)
                        {
                            result.startPosition = result.endPosition;
                        }

                        result.endPosition = Input.mousePosition;

                        result.status = InputStatus.Continuos;
                        result.selectedObject = null;
                        inputListeners.ForEach(l =>
                        {
                            l.Invoke(result);
                        });
                    }

                }
            }

            if (Input.GetButtonUp("Fire1"))
            {
                result.startPosition = result.endPosition;
                result.status = result.status == InputStatus.ObjectSelect ? InputStatus.ObjectSelect : InputStatus.TouchCompleted;
                inputContinuosTimer = 0f;
                inputListeners.ForEach(l =>
                {
                    l.Invoke(result);
                });
            }
        }
    }
}
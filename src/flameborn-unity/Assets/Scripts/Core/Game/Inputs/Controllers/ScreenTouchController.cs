using System;
using flameborn.Core.Game.Inputs.Controllers.Abstract;
using HF.Extensions;
using UnityEngine;

namespace flameborn.Core.Game.Inputs.Controllers
{
    [Serializable]
    public class ScreenTouchController : IInputController
    {
        // Public fields
        [field: SerializeField]
        private LayerMask RayMask { get; set; } = 0;

        // Private fields
        [SerializeField]
        private float inputContinuosDelay = 0.15f;

        [SerializeField]
        private float stepLength = 1f;

        private float inputContinuosTimer = 0f;
        private TouchResult result;

        /// <summary>
        /// Initializes a new instance of the ScreenTouchController class.
        /// </summary>
        /// <param name="rayMask">The layer mask for raycasting.</param>
        public ScreenTouchController(LayerMask rayMask)
        {
            RayMask = rayMask;
        }

        /// <summary>
        /// Handles the input and notifies the listeners.
        /// </summary>
        /// <param name="inputListeners">The input listeners to notify.</param>
        public void HandleInput(params Action<TouchResult>[] inputListeners)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InitializeTouchResult();
                result.startPosition = FindPosition();

                if (!FindSelectableObjects())
                {
                    result.startPosition = Input.mousePosition;
                }
            }

            if (Input.GetButton("Fire1"))
            {
                ProcessContinuousInput(inputListeners);
            }

            if (Input.GetButtonUp("Fire1"))
            {
                CompleteInput(inputListeners);
            }
        }

        private void InitializeTouchResult()
        {
            result = new TouchResult
            {
                status = InputStatus.Touched
            };
            inputContinuosTimer = 0f;
        }

        private void ProcessContinuousInput(Action<TouchResult>[] inputListeners)
        {
            inputContinuosTimer += Time.deltaTime;
            if (inputContinuosTimer >= inputContinuosDelay)
            {
                UpdateTouchResult();
                NotifyListeners(inputListeners);
            }
        }

        private void CompleteInput(Action<TouchResult>[] inputListeners)
        {
            result.status = result.status == InputStatus.ObjectSelect ? InputStatus.ObjectSelect : InputStatus.TouchCompleted;
            inputContinuosTimer = 0f;

            if (result.status == InputStatus.TouchCompleted)
            {
                result.endPosition = FindPosition();
            }
            else
            {
                result.startPosition = result.endPosition;
            }

            NotifyListeners(inputListeners);
        }

        private void UpdateTouchResult()
        {
            if (result.endPosition == Vector3.zero)
            {
                result.startPosition = Input.mousePosition;
            }

            if (result.endPosition != Vector3.zero && Vector3.Distance(result.endPosition, Input.mousePosition) >= 2f)
            {
                result.startPosition = result.endPosition;
            }

            result.endPosition = Input.mousePosition;

            if (result.endPosition != result.startPosition)
            {
                result.status = InputStatus.Continuos;
            }

            result.selectedObject = null;
        }

        private void NotifyListeners(Action<TouchResult>[] inputListeners)
        {
            inputListeners.ForEach(listener => listener.Invoke(result));
        }

        private Vector3 FindPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, RayMask))
            {
                return hitInfo.point;
            }

            return Vector3.zero;
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

        private bool IsMouseOnUI()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, RayMask))
            {
                return hitInfo.collider.gameObject.layer == 5;
            }

            return false;
        }
    }
}

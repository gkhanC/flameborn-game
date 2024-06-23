using flameborn.Core.Game.Inputs;
using flameborn.Core.Game.Inputs.Abstract;
using flameborn.Core.Game.Inputs.Controllers;
using UnityEngine;
using UnityEngine.AI;

namespace flameborn.Core.Game.Cameras
{
    /// <summary>
    /// Controls the camera movement and interactions.
    /// </summary>
    public class CameraController : MonoBehaviour, IInputListener<TouchResult>
    {
        // Public variables
        /// <summary>
        /// Global access to the CameraController.
        /// </summary>
        public static CameraController GlobalAccess { get; private set; }

        /// <summary>
        /// The magnitude of the camera movement.
        /// </summary>
        public float magnitude = 3f;

        /// <summary>
        /// The speed of the NavMeshAgent.
        /// </summary>
        public float speed = 20f;

        /// <summary>
        /// The NavMeshAgent component.
        /// </summary>
        public NavMeshAgent navMeshAgent;

        // Private variables
        private bool isPositionSet;
        private TouchResult result = new TouchResult();

        private void Awake()
        {
            GlobalAccess = this;
        }

        private void Start()
        {
            InputManager.GlobalAccess.SubscribeInputController(InputListener);
            navMeshAgent.speed = speed;
        }

        private void Update()
        {
            if (result.status == InputStatus.Continuos)
            {
                Vector2 deltaPos = result.endPosition - result.startPosition;
                Vector3 direction = new Vector3(-deltaPos.x, 0f, -deltaPos.y).normalized;
                navMeshAgent.SetDestination(transform.position + (direction * magnitude));
                isPositionSet = false;
            }
            else if (!isPositionSet)
            {
                navMeshAgent.SetDestination(transform.position);
            }
        }

        /// <summary>
        /// Sets the destination of the NavMeshAgent.
        /// </summary>
        /// <param name="position">The target position.</param>
        public void SetDestination(Vector3 position)
        {
            isPositionSet = true;
            navMeshAgent.SetDestination(position);
        }

        /// <summary>
        /// Adds speed to the NavMeshAgent.
        /// </summary>
        /// <param name="additionalSpeed">The speed to add.</param>
        public void AddSpeed(float additionalSpeed)
        {
            navMeshAgent.speed += additionalSpeed;
        }

        /// <summary>
        /// Adds magnitude to the camera movement.
        /// </summary>
        /// <param name="additionalMagnitude">The magnitude to add.</param>
        public void AddMagnitude(float additionalMagnitude)
        {
            magnitude += additionalMagnitude;
        }

        /// <summary>
        /// Handles input events.
        /// </summary>
        /// <param name="result">The touch result from input.</param>
        public void InputListener(TouchResult result)
        {
            this.result = result;
        }
    }
}

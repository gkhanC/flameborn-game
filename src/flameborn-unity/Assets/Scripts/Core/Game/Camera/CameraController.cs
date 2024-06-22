using flameborn.Core.Game.Inputs;
using flameborn.Core.Game.Inputs.Abstract;
using flameborn.Core.Game.Inputs.Controllers;
using UnityEngine;
using UnityEngine.AI;
namespace flameborn.Core.Game.Cameras
{
    public class CameraController : MonoBehaviour, IInputListener<TouchResult>
    {
        public static CameraController GlobalAccess { get; private set; }
        public float magnitude = 3f;
        public float speed = 20f;
        TouchResult result = new TouchResult();
        public NavMeshAgent navMeshAgent;

        private bool isPositionSet;

        private void Awake()
        {
            GlobalAccess = this;
        }

        void Start()
        {
            InputManager.GlobalAccess.SubscribeInputController(this.InputListener);
            navMeshAgent.speed = speed;
        }

        private void Update()
        {
            if (result.status == InputStatus.Continuos)
            {
                Vector2 deltaPos = result.endPosition - result.startPosition;
                Vector3 dir = new Vector3(-deltaPos.x, 0f, -deltaPos.y).normalized;
                navMeshAgent.SetDestination(transform.position + (dir * magnitude));
                isPositionSet = false;
            }
            else if (!isPositionSet)
            {
                navMeshAgent.SetDestination(transform.position);
            }
        }

        public void SetDestination(Vector3 position)
        {
            isPositionSet = true;
            navMeshAgent.SetDestination(position);
        }

        public void AddSpeed(float speed)
        {
            navMeshAgent.speed += speed;
        }

        public void AddMagnitude(float speed)
        {
            magnitude += speed;
        }

        public void InputListener(TouchResult result)
        {
            this.result = result;
        }
    }
}
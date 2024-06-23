using flameborn.Core.Game.Animation;
using flameborn.Core.Game.Events;
using flameborn.Core.Game.Objects.Abstract;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

namespace flameborn.Core.Game.Player
{
    /// <summary>
    /// Manages the player's worker actions and states.
    /// </summary>
    public class PlayerWorker : MonoBehaviourPun, IPunObservable, ISelectable
    {
        // Public variables
        /// <summary>
        /// The current animation information.
        /// </summary>
        public PlayerAnimation animationInfo = PlayerAnimation.None;

        /// <summary>
        /// The NavMeshAgent component.
        /// </summary>
        public NavMeshAgent agent;

        /// <summary>
        /// The animation controller for the player.
        /// </summary>
        public PlayerAnimationController animationController;

        /// <summary>
        /// The PhotonView component.
        /// </summary>
        public PhotonView photonView;

        /// <summary>
        /// The selectable type of the player.
        /// </summary>
        public SelectableTypes selectableType => SelectableTypes.Worker;

        public GameObject GetGameObject => gameObject;
        /// <summary>
        /// The target game object.
        /// </summary>
        public GameObject target;

        /// <summary>
        /// The current target for gathering.
        /// </summary>
        public GameObject prob;

        /// <summary>
        /// The wood game object.
        /// </summary>
        public GameObject wood;

        public bool isGathered, isGathering, isMovingAroundProb;

        // Private variables
        private float stuckSensibility = 0.1f;
        private float stuckTime = 0.25f;
        private float stuckTimer = 0.0f;
        private Vector3 lastPosition;
        private PlayerCampFire campFire;
        private bool woodIsActive;

        private void Update()
        {
            if (prob != null)
            {
                HandleGathering();
            }

            if (agent.hasPath)
            {
                if (agent.remainingDistance < 0.1f && animationInfo == PlayerAnimation.Run)
                {
                    PlayAnimation(PlayerAnimation.Idle);
                    agent.SetDestination(transform.position);
                }
                else if (animationInfo == PlayerAnimation.Run)
                {
                    CheckIfStuck();
                }
            }
        }

        private void HandleGathering()
        {
            if (!isGathered)
            {
                if (!isMovingAroundProb)
                {
                    lastPosition = transform.position;
                    stuckTimer = 0f;
                    agent.SetDestination(prob.transform.position);
                    PlayAnimation(PlayerAnimation.Run);
                    isMovingAroundProb = true;
                }
                else if (!isGathering && Vector3.Distance(transform.position, prob.transform.position) < 1.5f)
                {
                    agent.SetDestination(transform.position);
                    PlayAnimation(PlayerAnimation.Gather);
                }
            }
            else
            {
                if (campFire == null)
                {
                    campFire = EventManager.GlobalAccess.localPlayer;
                }

                if (Vector3.Distance(transform.position, campFire.transform.position) > 1f)
                {
                    if (animationInfo != PlayerAnimation.Run)
                    {
                        lastPosition = transform.position;
                        stuckTimer = 0f;
                        agent.SetDestination(campFire.transform.position);
                        animationController.PlayAnimation(PlayerAnimation.Run, true);
                    }
                }
                else
                {
                    campFire.AddWoods(5);
                    isGathered = false;
                    wood.SetActive(false);
                    woodIsActive = false;
                }
            }
        }

        /// <summary>
        /// Starts the gathering process.
        /// </summary>
        public void GatherStart()
        {
            isGathering = true;
        }

        /// <summary>
        /// Completes the gathering process.
        /// </summary>
        public void GatherCompleted()
        {
            isGathered = true;
            isGathering = false;
            isMovingAroundProb = false;
            wood.SetActive(true);
            woodIsActive = true;
        }

        private void CheckIfStuck()
        {
            if (!photonView.IsMine) return;

            stuckTimer += Time.deltaTime;
            if (stuckTimer >= stuckTime)
            {
                if (Vector3.Distance(lastPosition, transform.position) < stuckSensibility)
                {
                    agent.SetDestination(transform.position);
                    PlayAnimation(PlayerAnimation.Idle);
                    stuckTimer = 0f;
                    if (isMovingAroundProb) isMovingAroundProb = false;
                }

                lastPosition = transform.position;
                stuckTimer = 0f;
            }
        }

        /// <summary>
        /// Sets the destination for the player.
        /// </summary>
        /// <param name="destination">The target destination.</param>
        public void SetDestination(Vector3 destination)
        {
            if (!photonView.IsMine) return;
            prob = null;
            lastPosition = transform.position;
            stuckTimer = 0f;
            agent.SetDestination(destination);
            PlayAnimation(PlayerAnimation.Run);
            isGathered = false;
            isGathering = false;
            isMovingAroundProb = false;
            woodIsActive = false;
            wood.SetActive(false);
        }

        private void PlayAnimation(PlayerAnimation animation)
        {
            animationController.PlayAnimation(animation, wood.activeSelf);
        }

        /// <summary>
        /// Placeholder for attack functionality.
        /// </summary>
        public void Attack()
        {
            // Attack functionality to be implemented
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting && photonView.IsMine)
            {
                stream.SendNext(animationInfo);
                stream.SendNext(woodIsActive);
            }
            else if (!photonView.IsMine && info.Sender != PhotonNetwork.LocalPlayer)
            {
                var anim = (PlayerAnimation)stream.ReceiveNext();
                var woodIs = (bool)stream.ReceiveNext();
                woodIsActive = woodIs;
                wood.SetActive(woodIs);
                PlayAnimation(anim);
            }
        }

        /// <summary>
        /// Selects the player worker.
        /// </summary>
        public void Select()
        {
            // Selection functionality to be implemented
        }

        /// <summary>
        /// Sets the target object.
        /// </summary>
        /// <param name="go">The target selectable object.</param>
        public void SetTarget(ISelectable go)
        {
            if (go.selectableType == SelectableTypes.Prob)
            {
                prob = go.GetGameObject;
            }
        }

        /// <summary>
        /// Deselects the player worker.
        /// </summary>
        public void DeSelect()
        {
            // Deselection functionality to be implemented
        }
    }
}

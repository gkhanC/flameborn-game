using System;
using DG.Tweening;
using flameborn.Core.Game.Animation;
using flameborn.Core.Game.Events;
using flameborn.Core.Game.Objects.Abstract;
using HF.Extensions;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

namespace flameborn.Core.Game.Player
{
    public class PlayerWorker : MonoBehaviourPun, IPunObservable, ISelectable
    {
        private float stuckTime = .25f;
        private float stuckTimer = 0.0f;
        private float stuckSensibility = .1f;
        private Vector3 lastPosition;

        public PhotonView photonView;
        public PlayerAnimation animationInfo = PlayerAnimation.None;
        public PlayerAnimationController animationController;
        public NavMeshAgent agent;
        public GameObject target;
        public GameObject prob;

        public bool isGathered, isMovingAroundProb, isGathering;
        public SelectableTypes selectableType => SelectableTypes.Worker;

        public GameObject GetGameObject => this.gameObject;
        private PlayerCampFire campFire;
        public GameObject wood;
        private bool woodIsActive;
        private void Update()
        {
            if (prob != null)
            {
                Gathering();
            }

            if (agent.hasPath)
            {
                if (agent.remainingDistance < .1f && animationInfo == PlayerAnimation.Run)
                {
                    PlayAnimation(PlayerAnimation.Idle);
                    agent.SetDestination(transform.position);
                }
                else if (animationInfo == PlayerAnimation.Run)
                {
                    CheckStuck();
                }
            }

        }

        private void Gathering()
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

        public void GatherStart()
        {
            isGathering = true;
        }

        public void GatherCompleted()
        {
            isGathered = true;
            isGathering = false;
            isMovingAroundProb = false;
            wood.SetActive(true);
            woodIsActive = true;
        }

        private void CheckStuck()
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

        public void Attack()
        {

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

        public void Select()
        {

        }

        public void SetTarget(ISelectable go)
        {
            if (go.selectableType == SelectableTypes.Prob)
            {
                prob = go.GetGameObject;
            }
        }

        public void DeSelect()
        {

        }


    }
}

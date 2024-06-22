using flameborn.Core.Game.Player;
using UnityEngine;

namespace flameborn.Core.Game.Animation
{
    public class PlayerAnimationController : MonoBehaviour
    {
        public PlayerWorker controller;
        public Animator animator;
        public bool isAttackAnim;
        public bool isIdle;

        public void PlayAnimation(PlayerAnimation animation, bool isCarrying = false)
        {
            switch (animation)
            {
                case PlayerAnimation.Idle:
                    isIdle = true;
                    if (isCarrying)
                    {
                        animator.Play("carryidle");
                        controller.animationInfo = PlayerAnimation.Idle;
                        break;
                    }

                    animator.Play("idle");
                    controller.animationInfo = PlayerAnimation.Idle;
                    break;

                case PlayerAnimation.Run:
                    isIdle = false;
                    if (isCarrying)
                    {
                        animator.Play("carryrun");
                        controller.animationInfo = PlayerAnimation.Run;
                        break;
                    }

                    animator.Play("run");
                    controller.animationInfo = PlayerAnimation.Run;
                    break;
                case PlayerAnimation.Gather:
                    animator.Play("gather");
                    controller.animationInfo = PlayerAnimation.Gather;
                    break;
                case PlayerAnimation.Attack:
                    animator.Play("attack");
                    controller.animationInfo = PlayerAnimation.Attack;
                    isAttackAnim = true;
                    break;

                default: break;
            }
        }

        public void Gather()
        {
            controller.GatherStart();
        }

        public void GatherCompleted()
        {
            PlayAnimation(PlayerAnimation.Idle, true);
            controller.GatherCompleted();
        }

        public void AttackCompleted()
        {
            controller.Attack();
        }
    }
}
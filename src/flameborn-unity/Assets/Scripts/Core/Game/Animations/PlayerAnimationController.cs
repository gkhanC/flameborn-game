using flameborn.Core.Game.Player;
using UnityEngine;

namespace flameborn.Core.Game.Animation
{
    /// <summary>
    /// Controls the animations for the player character.
    /// </summary>
    public class PlayerAnimationController : MonoBehaviour
    {
        // Public variables
        /// <summary>
        /// Indicates if the attack animation is playing.
        /// </summary>
        public bool isAttackAnim;

        /// <summary>
        /// Indicates if the player is in idle state.
        /// </summary>
        public bool isIdle;

        /// <summary>
        /// The Animator component.
        /// </summary>
        public Animator animator;

        /// <summary>
        /// The PlayerWorker controller.
        /// </summary>
        public PlayerWorker controller;

        /// <summary>
        /// Plays the specified animation.
        /// </summary>
        /// <param name="animation">The animation to play.</param>
        /// <param name="isCarrying">Indicates if the player is carrying something.</param>
        public void PlayAnimation(PlayerAnimation animation, bool isCarrying = false)
        {
            switch (animation)
            {
                case PlayerAnimation.Idle:
                    isIdle = true;
                    animator.Play(isCarrying ? "carryidle" : "idle");
                    break;

                case PlayerAnimation.Run:
                    isIdle = false;
                    animator.Play(isCarrying ? "carryrun" : "run");
                    break;

                case PlayerAnimation.Gather:
                    animator.Play("gather");
                    break;

                case PlayerAnimation.Attack:
                    animator.Play("attack");
                    isAttackAnim = true;
                    break;

                default:
                    break;
            }
            controller.animationInfo = animation;
        }

        /// <summary>
        /// Starts the gather animation.
        /// </summary>
        public void Gather()
        {
            controller.GatherStart();
        }

        /// <summary>
        /// Completes the gather animation and switches to idle animation.
        /// </summary>
        public void GatherCompleted()
        {
            PlayAnimation(PlayerAnimation.Idle, true);
            controller.GatherCompleted();
        }

        /// <summary>
        /// Completes the attack animation.
        /// </summary>
        public void AttackCompleted()
        {
            controller.Attack();
        }
    }
}

using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace HF.TwoD.Character.Controllers
{
    /// <summary>
    /// Controller for managing the running behavior of a 2D character.
    /// </summary>
    [System.Serializable]
    public class CharacterRunController
    {
        /// <summary>
        /// Reference to the character's Rigidbody2D component.
        /// </summary>
        [SerializeField][Required] private Rigidbody2D m_Rigidbody2D;

        /// <summary>
        /// Data asset containing parameters for character running behavior.
        /// </summary>
        [Tooltip("Data asset containing parameters for character running behavior.")]
        [field: SerializeField]
        [Required("Data object is required")]
        public CharacterRunData Data { get; private set; } = null;

        /// <summary>
        /// On Running Event distributor
        /// </summary>
        /// <typeparam name="object">sender</typeparam>
        /// <typeparam name="bool">isRunning</typeparam>
        /// <returns></returns>
        [Space(10f)]
        [SerializeField]
        public UnityEvent<object, bool> OnRunningEvent = new UnityEvent<object, bool>();

        public CharacterRunController(Rigidbody2D rigidbody)
        {
            this.m_Rigidbody2D = rigidbody;
            OnRunningEvent = new UnityEvent<object, bool>();
        }

        public void SetRigidbody2D(Rigidbody2D rigidbody2D)
        {
            m_Rigidbody2D = rigidbody2D;
        }

        public void SetData(CharacterRunData data)
        {
            Data = data;
        }

        /// <summary>
        /// Executes running behavior for the character.
        /// </summary>
        /// <param name="directionX">Horizontal direction (-1 for left, 1 for right).</param>
        /// <param name="lerpAmount">Amount of smoothing to apply to the velocity change (default: 1).</param>
        /// <param name="isGrounded">Whether the character is grounded (default: true).</param>
        /// <param name="useAdditionalAcceleration">Whether to apply additional acceleration (default: false).</param>
        /// <param name="additionalAccelerationThreshold">Threshold for applying additional acceleration (default: 0).</param>
        /// <param name="additionalAccelerationRateMultiplier">Multiplier for additional acceleration rate (default: 1).</param>
        /// <param name="additionalSpeedMultiplier">Multiplier for additional speed (default: 1).</param>
        public void Run(float directionX, float lerpAmount = 1f, bool isGrounded = true, bool useAdditionalAcceleration = false, float additionalAccelerationThreshold = 0f, float additionalAccelerationRateMultiplier = 1f, float additionalSpeedMultiplier = 1f)
        {
            if (Data == null) return;

            float targetSpeed = directionX * Data.Speed;
            targetSpeed = Mathf.Lerp(m_Rigidbody2D.velocity.x, targetSpeed, lerpAmount);

            #region Calculating Acceleration Rate

            float accelerationRate = 0f;

            if (isGrounded)
                accelerationRate = (Mathf.Abs(targetSpeed) > .01f) ? Data.Acceleration : Data.Deceleration;
            else
                accelerationRate = (Mathf.Abs(targetSpeed) > .01f) ? Data.AccelerationInAir : Data.DecelerationInAir;

            #endregion

            #region Calculating Additional Speed & Acceleration Rate

            if (useAdditionalAcceleration && Mathf.Abs(m_Rigidbody2D.velocity.y) < additionalAccelerationThreshold)
            {
                accelerationRate *= additionalAccelerationRateMultiplier;
                targetSpeed *= additionalSpeedMultiplier;
            }

            #endregion

            #region Conserve Momentum

            if (Data.IsConserveMomentum && Mathf.Abs(m_Rigidbody2D.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(m_Rigidbody2D.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && !isGrounded)
            {
                accelerationRate = 0f;
            }

            #endregion

            float speedDifference = targetSpeed - m_Rigidbody2D.velocity.x;
            float movement = speedDifference * accelerationRate;
            m_Rigidbody2D.AddForce(movement * Vector2.right, ForceMode2D.Force);
            OnRunningEvent.Invoke(this, (Mathf.Abs(m_Rigidbody2D.velocity.x) > 0f));
        }
    }
}

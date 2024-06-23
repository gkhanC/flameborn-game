using UnityEngine;

namespace HF.TwoD.Character.Controllers
{
    /// <summary>
    /// Data asset for controlling the running behavior of the character.
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "RunData", menuName = "Hype Fire/2D/Create Run Data")]
    public class CharacterRunData : ScriptableObject
    {
        /// <summary>
        /// Speed of the character while running.
        /// </summary>
        [Tooltip("Speed of the character while running.")]
        public float Speed;

        [Space(5f)]
        [Header("Acceleration Control")]

        /// <summary>
        /// Base acceleration rate.
        /// </summary>
        [Tooltip("Base acceleration rate.")]
        public float Acceleration;

        /// <summary>
        /// Additional acceleration applied based on input.
        /// </summary>
        [Tooltip("Additional acceleration applied based on input.")]
        [HideInInspector]
        [field : SerializeField]
        public float AccelerationAmount { get; protected set; } = 0f;

        /// <summary>
        /// Decceleration rate when no input is applied.
        /// </summary>
        [Tooltip("Decceleration rate when no input is applied.")]
        [Space(5f)]
        public float Deceleration;

        /// <summary>
        /// Additional deceleration applied based on input.
        /// </summary>
        [Tooltip("Additional deceleration applied based on input.")]
         [HideInInspector]
        [field : SerializeField]
        public float DecelerationAmount { get; protected set; } = 0f;

        [Space(5f)]
        [Header("Air Control")]

        /// <summary>
        /// Acceleration rate while the character is in the air.
        /// </summary>
        [Tooltip("Acceleration rate while the character is in the air.")]
        [Range(0f, 1f)]
        public float AccelerationInAir;

        /// <summary>
        /// Decceleration rate while the character is in the air.
        /// </summary>
        [Tooltip("Decceleration rate while the character is in the air.")]
        [Range(0f, 1f)]
        public float DecelerationInAir;

        /// <summary>
        /// Determines whether the character's momentum should be conserved.
        /// </summary>
        [Tooltip("Determines whether the character's momentum should be conserved.")]
        public bool IsConserveMomentum = true;

#if UNITY_EDITOR
        [ContextMenu("Set Like Celeste")]
        public void SetLikeCeleste()
        {
            Speed = 11;
            Acceleration = 2.5f;
            Deceleration = 5f;
            AccelerationInAir = .65f;
            DecelerationInAir = .65f;
            IsConserveMomentum = true;
            OnValidate();
        }

        [ContextMenu("Set Like Hollow Knight")]
        public void SetLikeHollowKnight()
        {
            Speed = 9.5f;
            Acceleration = 9.5f;
            Deceleration = 9.5f;
            AccelerationInAir = 1f;
            DecelerationInAir = 1f;
            IsConserveMomentum = false;
            OnValidate();
        }

        [ContextMenu("Set Like Super Meat Boy")]
        public void SetLikeSuperMeatBoy()
        {
            Speed = 25f;
            Acceleration = 2f;
            Deceleration = 10f;
            AccelerationInAir = 1f;
            DecelerationInAir = 0f;
            IsConserveMomentum = true;
            OnValidate();
        }

        private void OnValidate()
        {
            AccelerationAmount = (50f * Acceleration) / Speed;
            DecelerationAmount = (50f * Deceleration) / Speed;
        }
#endif
    }
}

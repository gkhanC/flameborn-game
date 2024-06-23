using UnityEngine;

namespace HF.TwoD.Character
{
    /// <summary>
    /// This ScriptableObject stores data related to character jumping mechanics. 
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "JumpData", menuName = "Hype Fire/2D/Create Jump Data")]
    public class CharacterJumpData : ScriptableObject
    {
        /// <summary>
        /// Main jump settings used to determine the character's jump behavior.
        /// </summary>
        [Tooltip("The initial force applied to the character when jumping.")]
        [field: SerializeField]
        [HideInInspector]
        public float Force { get; protected set; } = 0f;

        [field: SerializeField]
        [HideInInspector]
        public float GravityStrength { get; protected set; } = 0f;

        [Header("Main Jump Settings")]
        [Tooltip("The maximum height the character reaches during a jump.")]
        public float MaxHeight;

        [Tooltip("The time it takes for the character to reach the apex of their jump.")]
        public float TimeToApex;

        /// <summary>
        /// Additional jump settings for fine-tuning jump behavior.
        /// </summary>
        [Space(10f)]
        [Header("Other Settings")]

        [Tooltip("Time threshold for determining if the character is hanging in the air during a jump.")]
        public float HangTimeThreshold;

        [Tooltip("Multiplier for acceleration while hanging in the air during a jump.")]
        public float HangAccelerationMultiplier;

        [Tooltip("Multiplier for gravity while approaching the apex of a jump.")]
        [Range(0f, 1f)]
        public float HangGravityMultiplier; //Reduces gravity while close to the apex (desired max height) of the jump

        [Tooltip("Maximum speed multiplier while hanging in the air during a jump.")]
        public float HangMaxSpeedMultiplier;

        [Tooltip("Multiplier for gravity after cutting off the jump.")]
        public float CutGravityMultiplier;

        /// <summary>
        /// Assist functionalities to improve jump responsiveness.
        /// </summary>
        [Space(10f)]
        [Header("Assists")]

        [Tooltip("Grace period after leaving a platform where a jump can still be performed.")]
        [Range(0.01f, 0.5f)] public float CoyoteTime;

        [Tooltip("Grace period after pressing jump where a jump can be automatically performed if conditions are met.")]
        [Range(0.01f, 0.5f)] public float InputBufferTime;


        [ContextMenu("Set Like Celeste")]
        public void SetLikeCeleste()
        {
            MaxHeight = 3.5f;
            TimeToApex = .3f;
            CutGravityMultiplier = 2f;
            HangGravityMultiplier = .5f;
            HangTimeThreshold = 1f;
            HangAccelerationMultiplier = 1.1f;
            HangMaxSpeedMultiplier = 1.3f;
            CoyoteTime = .1f;
            InputBufferTime = .1f;
            OnValidate();
        }

        [ContextMenu("Set Like Hollow Knight")]
        public void SetLikeHollowKnight()
        {
            MaxHeight = 6.5f;
            TimeToApex = .5f;
            CutGravityMultiplier = 3.5f;
            HangGravityMultiplier = 1f;
            HangTimeThreshold = 0f;
            HangAccelerationMultiplier = 1f;
            HangMaxSpeedMultiplier = 1f;
            CoyoteTime = .2f;
            InputBufferTime = .2f;
            OnValidate();
        }


        [ContextMenu("Set Like Super Meat Boy")]
        public void SetLikeSuperMeatBoy()
        {
            MaxHeight = 4.5f;
            TimeToApex = .45f;
            CutGravityMultiplier = 4f;
            HangGravityMultiplier = 0f;
            HangTimeThreshold = 0f;
            HangAccelerationMultiplier = 0f;
            HangMaxSpeedMultiplier = 0f;
            CoyoteTime = .2f;
            InputBufferTime = .2f;
            OnValidate();
        }

        private void OnValidate()
        {
            GravityStrength = -(2 * MaxHeight) / (TimeToApex * TimeToApex);
            Force = Mathf.Abs(GravityStrength) * TimeToApex;
        }
    }
}

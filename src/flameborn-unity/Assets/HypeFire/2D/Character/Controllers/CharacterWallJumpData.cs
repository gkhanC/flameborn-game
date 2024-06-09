using UnityEngine;

namespace HF.TwoD.Character
{
    /// <summary>
    /// This ScriptableObject stores data related to character wall jumping mechanics. 
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "WallJumpData", menuName = "Hype Fire/2D/Create Wall Jump Data")]
    public class CharacterWallJumpData : ScriptableObject
    {
        /// <summary>
        /// Settings related to wall jumping.
        /// </summary>
        [Space(10f)]
        [Header("Wall Jump")]
        [Tooltip("Determines whether the player rotates to face the wall jumping direction.")]
        public bool IsCanWallJump;

        [Tooltip("The force applied to the player when performing a wall jump.")]
        public Vector2 JumpForce;

        [Range(0f, 1f)]
        [Tooltip("Reduces the effect of player movement while wall jumping.")]
        public float JumpRunLerp;

        [Range(0f, 1f)]
        [Tooltip("The duration of the movement slowdown after performing a wall jump.")]
        public float JumpTime;

        [ContextMenu("Set Like Celeste")]
        public void SetLikeCeleste()
        {
            JumpForce = new Vector2(15f, 25f);
            JumpRunLerp = .05f;
            JumpTime = .15f;
        }

        [ContextMenu("Set Like Hollow Knight")]
        public void SetLikeHollowKnight()
        {
            JumpForce = new Vector2(8.5f, 20f);
            JumpRunLerp = .075f;
            JumpTime = .3f;
        }

        [ContextMenu("Set Like Super Meat Boy")]
        public void SetLikeSuperMeatBoy()
        {
            JumpForce = new Vector2(20f, 20f);
            JumpRunLerp = .075f;
            JumpTime = .2f;
        }
    }
}
using UnityEngine;

namespace HF.TwoD.Character
{
    /// <summary>
    /// This ScriptableObject stores data related to character dashing mechanics.
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "DashData", menuName = "Hype Fire/2D/Create Dash Data", order = 0)]
    public class CharacterDashData : ScriptableObject
    {
        /// <summary>
        /// The initial movement speed of the character while dashing.
        /// </summary>
        [Tooltip("The initial movement speed of the character while sliding.")]
        public float DashSpeed;

        public float DashTime = .25f;
        public float DashBufferTime = .15f;

        /// <summary>
        /// The acceleration applied to the character's movement speed during a dash.
        /// </summary>
        [Tooltip("The acceleration applied to the character's movement speed during a slide.")]
        public float DashAcceleration;

        /// <summary>
        /// Sets the initial dash speed and acceleration values similar to Celeste.
        /// </summary>
        [ContextMenu("Set Like Celeste")]
        public void SetLikeCeleste()
        {
            DashSpeed = 0f;
            DashAcceleration = 0f;
        }

        /// <summary>
        /// Sets the initial dash speed and acceleration values similar to Hollow Knight.
        /// </summary>
        [ContextMenu("Set Like Hollow Knight")]
        public void SetLikeHollowKnight()
        {
            DashSpeed = 12f;
            DashAcceleration = 12f;
        }

        /// <summary>
        /// Sets the initial dash speed and acceleration values similar to Super Meat Boy.
        /// </summary>
        [ContextMenu("Set Like Super Meat Boy")]
        public void SetLikeSuperMeatBoy()
        {
            DashSpeed = 15f;
            DashAcceleration = 3f;
        }
    }
}

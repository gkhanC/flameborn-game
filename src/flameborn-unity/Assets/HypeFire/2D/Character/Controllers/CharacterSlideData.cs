using UnityEngine;

namespace HF.TwoD.Character
{
    /// <summary>
    /// This ScriptableObject stores data related to character sliding mechanics.
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "SlideData", menuName = "Hype Fire/2D/Create Slide Data", order = 0)]
    public class CharacterSlideData : ScriptableObject
    {
        /// <summary>
        /// The initial movement speed of the character while sliding.
        /// </summary>
        [Tooltip("The initial movement speed of the character while sliding.")]
        public float SlideSpeed;

        /// <summary>
        /// The acceleration applied to the character's movement speed during a slide.
        /// </summary>
        [Tooltip("The acceleration applied to the character's movement speed during a slide.")]
        public float SlideAcceleration;

        /// <summary>
        /// Sets the initial slide speed and acceleration values similar to Celeste.
        /// </summary>
        [ContextMenu("Set Like Celeste")]
        public void SetLikeCeleste()
        {
            SlideSpeed = 0f;
            SlideAcceleration = 0f;
        }

        /// <summary>
        /// Sets the initial slide speed and acceleration values similar to Hollow Knight.
        /// </summary>
        [ContextMenu("Set Like Hollow Knight")]
        public void SetLikeHollowKnight()
        {
            SlideSpeed = -12f;
            SlideAcceleration = 12f;
        }

        /// <summary>
        /// Sets the initial slide speed and acceleration values similar to Super Meat Boy.
        /// </summary>
        [ContextMenu("Set Like Super Meat Boy")]
        public void SetLikeSuperMeatBoy()
        {
            SlideSpeed = -15f;
            SlideAcceleration = 3f;
        }
    }
}

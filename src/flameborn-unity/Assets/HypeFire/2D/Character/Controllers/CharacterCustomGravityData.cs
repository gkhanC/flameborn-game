using UnityEngine;

namespace HF.TwoD.Gravity
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "CharacterCustomGravityData", menuName = "Hype Fire/2D/Create Character Custom Gravity Data")]
    public class CharacterCustomGravityData : ScriptableObject
    {
        public float fallGravityMultiplier; //Multiplier to the player's gravityScale when falling.
        public float maxFallSpeed; //Maximum fall speed (terminal velocity) of the player when falling.

        [Space(5)]
        public float fastFallGravityMultiplier; //Larger multiplier to the player's gravityScale when they are falling and a downwards input is pressed.
                                                //Seen in games such as Celeste, lets the player fall extra fast if they wish.
        public float maxFastFallSpeed; //Maximum fall speed(terminal velocity) of the player when performing a faster fall.

        /// <summary>
        /// Sets the initial custom gravity values similar to Celeste.
        /// </summary>
        [ContextMenu("Set Like Celeste")]
        public void SetLikeCeleste()
        {
            fallGravityMultiplier = 1.5f;
            maxFallSpeed = 25f;
            fastFallGravityMultiplier = 2f;
            maxFastFallSpeed = 30f;
        }

        /// <summary>
        /// Sets the initial custom gravity values similar to Hollow Knight.
        /// </summary>
        [ContextMenu("Set Like Hollow Knight")]
        public void SetLikeHollowKnight()
        {
            fallGravityMultiplier = 2f;
            maxFallSpeed = 18f;
            fastFallGravityMultiplier = 1f;
            maxFastFallSpeed = 20f;
        }

        /// <summary>
        /// Sets the initial custom gravity values similar to Super Meat Boy.
        /// </summary>
        [ContextMenu("Set Like Super Meat Boy")]
        public void SetLikeSuperMeatBoy()
        {
            fallGravityMultiplier = 2f;
            maxFallSpeed = 20f;
            fastFallGravityMultiplier = 2f;
            maxFastFallSpeed = 20f;
        }

    }
}